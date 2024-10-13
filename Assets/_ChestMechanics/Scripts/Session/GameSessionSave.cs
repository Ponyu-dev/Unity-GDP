using Cysharp.Threading.Tasks;
using SaveSystem.Base;
using SaveSystem.Config;
using VContainer;

namespace _ChestMechanics.Session
{
    public class GameSessionSave : SaveLoadService
    {
        private const string FileName = "gameSession.data";
        
        [Inject]
        public GameSessionSave(SaveConfig saveConfig) : base(saveConfig)
        {
            _saveFileName = FileName;
        }
        
        public async UniTask SaveUnitsAsync(GameSessionData unitsData)
        {
            await SaveAsync(unitsData);
        }

        public UniTask<GameSessionData> LoadUnitsAsync()
        {
            return LoadAsync<GameSessionData>();
        }
    }
}