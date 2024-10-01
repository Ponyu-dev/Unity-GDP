using System;
using UnityEngine;
using System.Linq;
using Cysharp.Threading.Tasks;
using VContainer;
using VContainer.Unity;
using GameEngine.Data;
using SaveSystem.Base;
using Object = UnityEngine.Object;

namespace GameEngine
{
    public sealed class EntryPoint : IStartable, IDisposable
    {
        private readonly UnitManager _unitManager;
        private readonly ResourceService _resourceService;
        private readonly ISaveLoadService _saveLoadService;

        [Inject]
        public EntryPoint(UnitManager unitManager, ResourceService resourceService, ISaveLoadService saveLoadService)
        {
            _unitManager = unitManager;
            _resourceService = resourceService;
            _saveLoadService = saveLoadService;
            
            // Подписка на события сохранения и загрузки
            _saveLoadService.SaveCompleted += OnSaveCompleted;
            _saveLoadService.SaveFailed += OnSaveFailed;
            _saveLoadService.LoadCompleted += OnLoadCompleted;
            _saveLoadService.LoadFailed += OnLoadFailed;
        }

        public void Start()
        {
            // Инициализация юнитов и ресурсов при старте
            _unitManager.SetupUnits(Object.FindObjectsOfType<Unit>());
            _resourceService.SetResources(Object.FindObjectsOfType<Resource>());

            LoadGameAsync().Forget();
        }

        // Метод для сохранения игры
        public async UniTask SaveGameAsync()
        {
            var saveData = new SaveData
            {
                Resources = _resourceService.GetResources().Select(r => new ResourceData
                {
                    ID = r.ID,
                    Amount = r.Amount
                }).ToList(),

                Units = _unitManager.GetAllUnits().Select(u => new UnitData
                {
                    Name = u.name,
                    Type = u.Type,
                    HitPoints = u.HitPoints,
                    Position = new Vector3Data(u.Position),
                    Rotation = new Vector3Data(u.Rotation)
                }).ToList()
            };

            await _saveLoadService.SaveAsync(saveData);
        }

        // Метод для загрузки игры
        public async UniTask LoadGameAsync()
        {
            var saveData = await _saveLoadService.LoadAsync<SaveData>();
            if (saveData == null) return;
            
            ApplySaveData(saveData);
        }

        private void ApplySaveData(SaveData data)
        {
            // Применение данных ресурсов
            foreach (var resourceData in data.Resources)
            {
                var resource = _resourceService.GetResources().FirstOrDefault(r => r.ID == resourceData.ID);
                if (resource != null)
                {
                    resource.Amount = resourceData.Amount;
                }
                else
                {
                    Debug.LogWarning($"Resource with ID {resourceData.ID} not found.");
                }
            }

            // Применение данных юнитов
            foreach (var unitData in data.Units)
            {
                var unit = _unitManager.GetAllUnits().FirstOrDefault(u => u.name == unitData.Name);
                if (unit != null)
                {
                    unit.HitPoints = unitData.HitPoints;
                    unit.gameObject.transform.position = unitData.Position.ToVector3();
                    unit.gameObject.transform.eulerAngles = unitData.Rotation.ToVector3();
                }
                else
                {
                    Debug.LogWarning($"Unit of type {unitData.Type} not found.");
                }
            }
        }

        private void OnSaveCompleted()
        {
            Debug.Log("Игра успешно сохранена.");
        }

        private void OnSaveFailed()
        {
            Debug.LogError("Не удалось сохранить игру.");
        }

        private void OnLoadCompleted()
        {
            Debug.Log("Игра успешно загружена.");
        }

        private void OnLoadFailed(Exception ex)
        {
            Debug.LogError($"Ошибка загрузки игры: {ex.Message}");
        }
        
        public void Dispose()
        {
            // Отписка от событий
            _saveLoadService.SaveCompleted -= OnSaveCompleted;
            _saveLoadService.SaveFailed -= OnSaveFailed;
            _saveLoadService.LoadCompleted -= OnLoadCompleted;
            _saveLoadService.LoadFailed -= OnLoadFailed;
        }
    }
}