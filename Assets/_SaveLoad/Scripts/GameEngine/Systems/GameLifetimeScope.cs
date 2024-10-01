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
        private UnitManager unitManager;
        
        [SerializeField]
        private ResourceService resourceService;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(saveConfig);
            builder.Register<EncryptionUtils>(Lifetime.Singleton);
            
            builder.RegisterInstance(unitManager);
            builder.RegisterInstance(resourceService);
            
            builder.Register<SaveLoadService>(Lifetime.Singleton).AsImplementedInterfaces();
            
            builder.Register<EntryPoint>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            
            builder.RegisterComponent(testSave);
        }
    }
}