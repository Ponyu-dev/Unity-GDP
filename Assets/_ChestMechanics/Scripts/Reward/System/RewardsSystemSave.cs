using _ChestMechanics.Scripts.Reward.Data;
using Cysharp.Threading.Tasks;
using SaveSystem.Base;
using SaveSystem.Config;
using VContainer;

namespace _ChestMechanics.Scripts.Reward.System
{
    public class RewardsSystemSave : SaveLoadService
    {
        private const string FileName = "rewards.data";
        
        [Inject]
        public RewardsSystemSave(SaveConfig saveConfig) : base(saveConfig)
        {
            _saveFileName = FileName;
        }
        
        public async UniTask SaveUnitsAsync(RewardSaveList rewardSave)
        {
            await SaveAsync(rewardSave);
        }

        public async UniTask<RewardSaveList> LoadUnitsAsync()
        {
            return await LoadAsync<RewardSaveList>();
        }
    }
}