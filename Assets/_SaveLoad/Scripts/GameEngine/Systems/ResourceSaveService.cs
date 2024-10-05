using Cysharp.Threading.Tasks;
using GameEngine.Data;
using SaveSystem.Base;
using SaveSystem.Config;
using VContainer;

namespace GameEngine
{
    public class ResourceSaveService : SaveLoadService
    {
        [Inject]
        public ResourceSaveService(SaveConfig saveConfig) : base(saveConfig)
        {
            _saveFileName = "resources.data";
        }
        
        public async UniTask SaveUnitsAsync(ResourcesData unitsData)
        {
            await SaveAsync(unitsData);
        }

        public async UniTask<ResourcesData> LoadUnitsAsync()
        {
            return await LoadAsync<ResourcesData>();
        }
    }
}