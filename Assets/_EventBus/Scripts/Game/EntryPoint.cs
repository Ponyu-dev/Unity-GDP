using _EventBus.Scripts.Game.Players;
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
            playersConfigure.Configure(builder);
        }
    }
}