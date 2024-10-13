using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace _ChestMechanics.Scripts.Reward.Data
{
    [Serializable]
    public class RewardSaveList
    {
        public List<RewardSave> rewardsSaves;
        
        public int AddToRewardCount(RewardType rewardType, int rewardCount)
        {
            var rewardSave = rewardsSaves.Find(it => it.TypeReward == rewardType.ToString());
            return rewardSave.AddRewardCount(rewardCount);
        }
    }
    
    [Serializable]
    public class RewardSave
    {
        public string TypeReward { get; private set; }
        public int Count { get; private set; }

        [JsonConstructor]
        public RewardSave(string typeReward, int count)
        {
            this.TypeReward = typeReward;
            this.Count = count;
        }
        
        public RewardSave(RewardData rewardData)
        {
            this.TypeReward = rewardData.TypeReward.ToString();
            this.Count = rewardData.CountDefault;
        }

        public int AddRewardCount(int rewardCount)
        {
            Count += rewardCount;

            return Count;
        }
    }
}