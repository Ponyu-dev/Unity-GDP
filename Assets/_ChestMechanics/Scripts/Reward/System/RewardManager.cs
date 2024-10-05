using System.Collections.Generic;
using _ChestMechanics.Scripts.Reward.Data;
using _ChestMechanics.Scripts.Reward.System;
using Cysharp.Threading.Tasks;
using Sirenix.Utilities;
using VContainer;
using VContainer.Unity;

namespace _ChestMechanics.Scripts.Reward.Presenter
{
    public interface IRewardManager
    {
        void UpdateRewardCount(RewardData[] rewardsData);
    }
    
    public class RewardManager : IInitializable, IRewardManager
    {
        private readonly RewardsConfig _rewardsConfig;
        private readonly RewardsSystemSave _rewardsSystemSave;
        private readonly IRewardFactory _rewardFactory;
        private RewardSaveList _rewardSaveList;

        private bool _processSave = false;

        private readonly Dictionary<string, IRewardPresenter> _rewardsPresenters = new();

        [Inject]
        public RewardManager(
            RewardsConfig rewardsConfig,
            RewardsSystemSave rewardsSystemSave,
            IRewardFactory rewardFactory)
        {
            _rewardsConfig = rewardsConfig;
            _rewardsSystemSave = rewardsSystemSave;
            _rewardFactory = rewardFactory;
        }

        public void Initialize()
        {
            InitializeRewardsAsync().Forget();
        }
        
        private async UniTaskVoid InitializeRewardsAsync()
        {
            _rewardSaveList = await _rewardsSystemSave.LoadUnitsAsync();
            
            if (_rewardSaveList?.rewardsSaves?.IsNullOrEmpty() ?? true)
                SpawnDefaultRewards(_rewardsConfig.GetRewards());
            else
                LoadSavedRewards(_rewardSaveList?.rewardsSaves);
        }

        private void SetNewRewardPresenter(RewardConfig reward, RewardSave rewardSave)
        {
            var rewardPresenter = _rewardFactory.Create(reward);
            _rewardsPresenters.Add(rewardSave.TypeReward, rewardPresenter);
            SetRewardCount(rewardSave.TypeReward, rewardSave.Count);
        }

        private void SpawnDefaultRewards(IEnumerable<RewardConfig> rewards)
        {
            _rewardSaveList = new RewardSaveList
            {
                rewardsSaves = new List<RewardSave>()
            };
            
            foreach (var reward in rewards)
            {
                var rewardSave = reward.SaveReward;
                
                if (_rewardsPresenters.ContainsKey(rewardSave.TypeReward)) continue;
                _rewardSaveList.rewardsSaves.Add(rewardSave);
                SetNewRewardPresenter(reward, rewardSave);
            }
            
            SaveRewardsAsync().Forget();
        }

        private void LoadSavedRewards(List<RewardSave> savedRewards)
        {
            foreach (var rewardSave in savedRewards)
            {
                if (string.IsNullOrEmpty(rewardSave.TypeReward)) continue;
                if (_rewardsPresenters.ContainsKey(rewardSave.TypeReward)) continue;
                
                var rewardConfig = _rewardsConfig.GetRewardByType(rewardSave.TypeReward);
                if (rewardConfig == null) continue;
                
                SetNewRewardPresenter(rewardConfig, rewardSave);
            }
        }

        private async UniTaskVoid SaveRewardsAsync()
        {
            _processSave = true;
            
            if (_rewardSaveList != null)
            {
                await _rewardsSystemSave.SaveUnitsAsync(_rewardSaveList);
            }

            _processSave = false;
        }

        private void SetRewardCount(string rewardType, int rewardCount)
        {
            if (_rewardsPresenters.TryGetValue(rewardType, out var rewardPresenter))
            {
                rewardPresenter.UpdateRewardCount(rewardCount.ToString());
            }
        }

        public void UpdateRewardCount(RewardData[] rewardsData)
        {
            if (_processSave) return;
            
            foreach (var rewardData in rewardsData)
            {
                var updatedCount = _rewardSaveList.AddToRewardCount(rewardData.TypeReward, rewardData.CountDefault);
                SetRewardCount(rewardData.TypeReward.ToString(), updatedCount);
            }
            
            SaveRewardsAsync().Forget();
        }
    }
}
