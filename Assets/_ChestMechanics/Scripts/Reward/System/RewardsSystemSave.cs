using SaveSystem.Base;
using VContainer;
using NotImplementedException = System.NotImplementedException;

namespace _ChestMechanics.Scripts.Reward.System
{
    public class RewardsSystemSave : IDataProvider<ISavableData>
    {
        [Inject]
        public RewardsSystemSave()
        {
            
        }
        
        /*public async UniTask SaveUnitsAsync(RewardSaveList rewardSave)
        {
            await SaveAsync(rewardSave);
        }

        public async UniTask<RewardSaveList> LoadUnitsAsync()
        {
            return await LoadAsync<RewardSaveList>();
        }*/
        public ISavableData GetDataForSaving()
        {
            throw new NotImplementedException();
        }

        public void ApplyLoadedData(ISavableData data)
        {
            throw new NotImplementedException();
        }
    }
}