using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using GameEngine.Data;
using GameEngine.Providers;
using SaveSystem.Base;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameEngine
{
    public interface ISaveLoadPoint
    {
        public UniTask SaveGameAsync();
        public UniTask LoadGameAsync();
    }
    
    public sealed class SaveLoadPoint : IStartable, ISaveLoadPoint
    {
        private readonly IEnumerable<IDataProvider<SavableData>> _dataProviders;
        private readonly ISaveLoadService _saveService;

        [Inject]
        public SaveLoadPoint(
            ISaveLoadService saveService,
            IDataProvider<ListUnitData> unitDataProvider,
            IEnumerable<IDataProvider<SavableData>> dataProviders)
        {
            Debug.Log("SaveLoadPoint Constructor");
            _saveService = saveService;
            _dataProviders = dataProviders;
            Debug.Log($"SaveLoadPoint Constructor {_dataProviders.Count()}");
            Debug.Log($"SaveLoadPoint Constructor {unitDataProvider.GetType()}");
        }

        public void Start()
        {
            Debug.Log("SaveLoadPoint Start");
            LoadGameAsync().Forget();
        }

        // Метод для сохранения игры
        public async UniTask SaveGameAsync()
        {
            Debug.Log("SaveLoadPoint SaveGameAsync");
            Debug.Log($"SaveLoadPoint SaveGameAsync {_dataProviders.Count()}");
            foreach (var provider in _dataProviders)
            {
                // Получаем данные для сохранения и сохраняем их через универсальный сервис
                var data = provider.GetDataForSaving();
                if (data != null)
                {
                    await _saveService.SaveAsync(data);
                }
            }
        }

        // Метод для загрузки игры
        public async UniTask LoadGameAsync()
        {
            Debug.Log("SaveLoadPoint LoadGameAsync");
            Debug.Log($"SaveLoadPoint LoadGameAsync {_dataProviders.Count()}");
            foreach (var provider in _dataProviders)
            {
                // Загружаем данные через сервис и передаем их провайдеру
                var type = provider.GetDataForSaving();
                var data = await _saveService.LoadAsync(type);
                if (data != null)
                {
                    provider.ApplyLoadedData(data);
                }
            }
        }
    }
}