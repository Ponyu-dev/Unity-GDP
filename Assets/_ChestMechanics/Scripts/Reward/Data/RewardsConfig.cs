using System.Collections.Generic;
using UnityEngine;

namespace _ChestMechanics.Scripts.Reward.Data
{
    [CreateAssetMenu(menuName = "Rewards/Config", fileName = "RewardsConfig", order = 0)]
    public class RewardsConfig : ScriptableObject
    {
        [SerializeField]
        private List<RewardConfig> rewardsList;

        public IReadOnlyList<RewardConfig> GetRewards() => rewardsList;

        public RewardConfig GetRewardByType(string rewardSaveTypeReward)
        {
            return rewardsList.Find(it => it.SaveReward.TypeReward == rewardSaveTypeReward);
        }
    }
}