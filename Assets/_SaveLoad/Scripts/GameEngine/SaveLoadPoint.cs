using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cysharp.Threading.Tasks;
using VContainer;
using VContainer.Unity;
using GameEngine.Data;
using Object = UnityEngine.Object;

namespace GameEngine
{
    public sealed class SaveLoadPoint : IStartable
    {
        private readonly UnitManager _unitManager;
        private readonly UnitSaveLoaderService _unitSaveLoaderService;
        private readonly UnitPrefabs _unitPrefabs;
        private readonly ResourceService _resourceService;
        private readonly ResourceSaveService _resourceSaveService;

        [Inject]
        public SaveLoadPoint(
            UnitManager unitManager,
            UnitSaveLoaderService unitSaveLoaderService,
            UnitPrefabs unitPrefabs,
            ResourceService resourceService,
            ResourceSaveService resourceSaveService)
        {
            _unitManager = unitManager;
            _unitSaveLoaderService = unitSaveLoaderService;
            _unitPrefabs = unitPrefabs;
            _resourceService = resourceService;
            _resourceSaveService = resourceSaveService;
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
            await _unitSaveLoaderService.SaveUnitsAsync(ListUnitData.Mapper(_unitManager.GetAllUnits().ToList()));
            await _resourceSaveService.SaveUnitsAsync(ResourcesData.Mapper(_resourceService.GetResources()));
        }

        // Метод для загрузки игры
        public async UniTask LoadGameAsync()
        {
            var listUnits = await _unitSaveLoaderService.LoadUnitsAsync();
            if (listUnits != null)
                ApplyUnits(listUnits.Units);
            
            var resources = await _resourceSaveService.LoadUnitsAsync();
            if (resources != null)
                ApplyResources(resources.resourcesData);

        }

        private void ApplyResources(List<ResourceData> resources)
        {
            foreach (var resourceData in resources)
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
        }

        private void ApplyUnits(List<UnitData> units)
        {
            // Создаем словарь для быстрого поиска существующих юнитов по имени
            var existingUnits = _unitManager.GetAllUnits().ToDictionary(u => u.name);

            // Проходим по всем сохраненным юнитам и обновляем или создаем их
            foreach (var unitData in units)
            {
                if (existingUnits.TryGetValue(unitData.Name, out var existingUnit))
                {
                    // Если юнит существует, обновляем его свойства
                    existingUnit.HitPoints = unitData.HitPoints;
                    existingUnit.gameObject.transform.position = unitData.Position.ToVector3();
                    existingUnit.gameObject.transform.eulerAngles = unitData.Rotation.ToVector3();
                    // Удаляем из словаря, чтобы не удалить позже
                    existingUnits.Remove(unitData.Name);
                }
                else
                {
                    // Если юнит не существует, спавним его
                    var unitPrefab = _unitPrefabs.GetPrefabByType(unitData.Type);
                    if (unitPrefab != null)
                    {
                        var newUnit = _unitManager.SpawnUnit(unitPrefab, unitData.Position.ToVector3(), Quaternion.Euler(unitData.Rotation.ToVector3()));
                        newUnit.HitPoints = unitData.HitPoints; // Устанавливаем здоровье нового юнита
                    }
                    else
                    {
                        Debug.LogWarning($"Префаб юнита типа {unitData.Type} не найден.");
                    }
                }
            }

            // Теперь удаляем юниты, которые остались в existingUnits
            foreach (var unit in existingUnits.Values.ToList())
            {
                _unitManager.DestroyUnit(unit);
            }
        }
    }
}