using UnityEngine;
using Zenject;

namespace SampleGame
{
    public sealed class PauseScreenInstaller : MonoInstaller
    {
        [SerializeField] private Transform parentContainer;
        
        public override void InstallBindings()
        {
            Container
                .Bind<PauseButton>()
                .FromComponentInHierarchy()
                .AsCached()
                .NonLazy();
            
            Container.BindInterfacesTo<ScreenInitializer<PauseScreen>>()
                .AsCached()
                .WithArguments(parentContainer, false)
                .NonLazy();
        }
    }
}