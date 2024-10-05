using _ChestMechanics.Chests.System;
using _ChestMechanics.Scripts.Reward.System;
using _ChestMechanics.Session;
using SaveSystem.Config;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _ChestMechanics.Scripts.System
{
    public class SystemLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private SaveConfig saveConfig;
        
        [SerializeField]
        private GameSessionConfigure gameSessionConfigure;
        [SerializeField]
        private ChestsConfigure chestsConfigure;
        [SerializeField]
        private RewardConfigure rewardConfigure;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(saveConfig);
            
            gameSessionConfigure.Configure(builder);
            chestsConfigure.Configure(builder);
            rewardConfigure.Configure(builder);
        }
    }
}