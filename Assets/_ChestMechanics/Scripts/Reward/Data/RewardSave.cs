using System;

namespace _ChestMechanics.Scripts.Reward.Data
{
    [Serializable]
    public class RewardSave
    {
        public string TypeReward { get; private set; }
        public int Count { get; private set; }

        public RewardSave(string rewardType, int count)
        {
            this.TypeReward = rewardType;
            this.Count = count;
        }
    }
}