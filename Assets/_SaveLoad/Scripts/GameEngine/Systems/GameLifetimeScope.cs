using GameEngine.Providers;
using SaveSystem.Base;
using SaveSystem.Config;
using SaveSystem.Utils;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameEngine
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private TestSave testSave;
        
        [SerializeField]
        private SaveConfig saveConfig;
        
        [SerializeField]
        private UnitPrefabs unitPrefabs;

        [SerializeField]
        private UnitManager unitManager;
        
        [SerializeField]
        private ResourceService resourceService;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(saveConfig);
            builder.RegisterInstance(unitPrefabs);
            
            builder.Register<EncryptionUtils>(Lifetime.Singleton);
            
            builder.RegisterInstance(unitManager);
            builder.RegisterInstance(resourceService);
            
            builder.Register<SaveLoadService>(Lifetime.Singleton)
                .AsImplementedInterfaces();
            
            builder.Register<UnitDataProvider>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();
            builder.Register<ResourcesDataProvider>(Lifetime.Singleton)
                .AsImplementedInterfaces()
                .AsSelf();
            
            builder.Register<SaveLoadPoint>(Lifetime.Singleton).AsImplementedInterfaces();
            
            builder.RegisterComponent(testSave);
        }
    }
}