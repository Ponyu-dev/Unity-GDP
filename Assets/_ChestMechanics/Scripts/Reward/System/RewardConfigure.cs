using System;
using _ChestMechanics.Scripts.Reward.Data;
using _ChestMechanics.Scripts.Reward.Presenter;
using UnityEngine;
using VContainer;

namespace _ChestMechanics.Scripts.Reward.System
{
    [Serializable]
    public class RewardConfigure
    {
        [SerializeField] private Transform container;
        [SerializeField] private RewardsConfig rewardsConfig;
        
        public void Configure(IContainerBuilder builder)
        {
            Debug.Log("RewardConfigure RegisterInstance RewardsConfig");
            builder.RegisterInstance(rewardsConfig);
            
            Debug.Log("RewardConfigure Register RewardPresenter");
            builder.Register<RewardPresenter>(Lifetime.Transient)
                .AsImplementedInterfaces()
                .AsSelf();
            
            Debug.Log("RewardConfigure Register RewardFactory");
            builder.Register<RewardFactory>(Lifetime.Singleton)
                .WithParameter("container", container)
                .AsImplementedInterfaces()
                .AsSelf();
            
            Debug.Log("RewardConfigure Register ListRewardPresenter");
            builder.Register<ListRewardPresenter>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();
        }
    }
}