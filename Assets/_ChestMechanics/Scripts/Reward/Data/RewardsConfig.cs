using System.Collections.Generic;
using UnityEngine;

namespace _ChestMechanics.Scripts.Reward.Data
{
    [CreateAssetMenu(menuName = "Rewards/Config", fileName = "RewardsConfig", order = 0)]
    public class RewardsConfig : ScriptableObject
    {
        [SerializeField]
        private List<RewardData> rewardsList;

        public IReadOnlyList<RewardData> GetChest() => rewardsList;
    }
}