using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SaveSystem.Base;
using VContainer;
using VContainer.Unity;

namespace _SaveLoad.Scripts.SaveSystem
{
    public interface ISaveLoadPoint
    {
        public UniTask SaveGameAsync();
        public UniTask LoadGameAsync();
    }
    
    public sealed class SaveLoadPoint : IStartable, ISaveLoadPoint
    {
        private readonly IEnumerable<IDataProvider<ISavableData>> _dataProviders;
        private readonly ISaveLoadService _saveService;

        [Inject]
        public SaveLoadPoint(
            ISaveLoadService saveService,
            IEnumerable<IDataProvider<ISavableData>> dataProviders)
        {
            _saveService = saveService;
            _dataProviders = dataProviders;
        }

        public void Start()
        {
            LoadGameAsync().Forget();
        }
        
        public async UniTask SaveGameAsync()
        {
            foreach (var provider in _dataProviders)
            {
                var data = provider.GetDataForSaving();
                await _saveService.SaveAsync(data, data.GetType().Name);
            }
        }
        
        public async UniTask LoadGameAsync()
        {
            foreach (var provider in _dataProviders)
            {
                var type = provider.GetDataForSaving();
                var data = await _saveService.LoadAsync(type.GetType().Name, type.GetType());
                if (data != null)
                    provider.ApplyLoadedData(data);
            }
        }
    }
}