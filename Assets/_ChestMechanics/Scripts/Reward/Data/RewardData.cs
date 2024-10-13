using System;
using UnityEngine;

namespace _ChestMechanics.Scripts.Reward.Data
{
    [Serializable]
    public class RewardData
    {
        [SerializeField] 
        private RewardType rewardType;
        public RewardType TypeReward => rewardType;

        [SerializeField] 
        private int countDefault;
        public int CountDefault => countDefault;
    }
    
    [Serializable]
    public class RewardConfig
    {
        [SerializeField]
        private RewardData rewardData;
        
        [SerializeField]
        private GameObject prefab;
        public GameObject Prefab => prefab;

        public RewardSave SaveReward => new RewardSave(rewardData);
    }
}