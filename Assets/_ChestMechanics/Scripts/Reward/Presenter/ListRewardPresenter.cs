using System.Collections.Generic;
using _ChestMechanics.Scripts.Reward.Data;
using _ChestMechanics.Scripts.Reward.System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _ChestMechanics.Scripts.Reward.Presenter
{
    public class ListRewardPresenter : IInitializable
    {
        private readonly RewardsConfig _rewardsConfig;
        private readonly IRewardFactory _rewardFactory;

        private readonly List<IRewardPresenter> _rewardsPresenters = new();

        [Inject]
        public ListRewardPresenter(
            RewardsConfig rewardsConfig,
            IRewardFactory rewardFactory)
        {
            Debug.Log("ListRewardPresenter Constructor");
            _rewardsConfig = rewardsConfig;
            _rewardFactory = rewardFactory;
        }

        public void Initialize()
        {
            SpawnChests(_rewardsConfig.GetChest());
        }
        
        private void SpawnChests(IEnumerable<RewardData> rewards)
        {
            foreach (var reward in rewards)
            {
                _rewardsPresenters.Add(_rewardFactory.Create(reward));
            }
        }
    }
}