using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameEngine.Data;
using SaveSystem.Base;
using SaveSystem.Config;
using VContainer;

namespace GameEngine
{
    public class UnitSaveLoaderService : SaveLoadService
    {
        [Inject]
        public UnitSaveLoaderService(SaveConfig saveConfig) : base(saveConfig)
        {
            _saveFileName = "units.data";
        }
        
        public async UniTask SaveUnitsAsync(ListUnitData unitsData)
        {
            await SaveAsync(unitsData);
        }

        public async UniTask<ListUnitData> LoadUnitsAsync()
        {
            return await LoadAsync<ListUnitData>();
        }
    }
}