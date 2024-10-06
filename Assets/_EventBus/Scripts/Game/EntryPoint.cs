using _EventBus.Scripts.Game.Configures;
using _EventBus.Scripts.ServiceEventBus;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _EventBus.Scripts.Game
{
    public class EntryPoint : LifetimeScope
    {
        [SerializeField] private PlayersConfigure playersConfigure;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<EventBus>(Lifetime.Singleton).AsSelf();
            
            playersConfigure.Configure(builder);
        }
    }
}