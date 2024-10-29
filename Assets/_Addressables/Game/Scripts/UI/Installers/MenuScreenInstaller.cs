using UnityEngine;
using Zenject;

namespace SampleGame
{
    public sealed class MenuScreenInstaller : MonoInstaller
    {
        [SerializeField] private Transform parentContainer;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ScreenInitializer<MenuScreen>>()
                .AsCached()
                .WithArguments(parentContainer, true)
                .NonLazy();
        }
    }
}