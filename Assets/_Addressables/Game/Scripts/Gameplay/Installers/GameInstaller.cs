using System;
using _Addressables.Game.Scripts.Gameplay.Locations.Trigger;
using _Addressables.Game.Scripts.Gameplay.Services;
using UnityEngine;
using Zenject;

namespace SampleGame
{
    public sealed class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private Transform worldContainer;
        
        [SerializeField]
        private CameraConfig cameraConfig;

        [SerializeField]
        private new Camera camera;
        
        [SerializeField]
        private InputConfig inputConfig;

        public override void InstallBindings()
        {
            this.Container.BindInterfacesTo<AddressablesService>()
                .AsSingle()
                .NonLazy();
            
            this.Container
                .Bind<Camera>()
                .FromInstance(this.camera);

            this.Container
                .Bind<ICharacter>()
                .FromComponentInHierarchy()
                .AsSingle();

            this.Container
                .BindInterfacesTo<MoveController>()
                .AsCached()
                .NonLazy();
            
            this.Container
                .Bind<IMoveInput>()
                .To<MoveInput>()
                .AsSingle()
                .WithArguments(this.inputConfig)
                .NonLazy();

            this.Container
                .BindInterfacesTo<CameraFollower>()
                .AsCached()
                .WithArguments(this.cameraConfig.cameraOffset)
                .NonLazy();
            
            this.Container.BindInterfacesTo<TriggerView>()
                .FromComponentsInHierarchy()
                .AsCached()
                .NonLazy();
            
            this.Container.Bind<TriggerPresenter>()
                .AsSingle()
                .WithArguments(worldContainer)
                .NonLazy();
        }

        private void OnDestroy()
        {
            // Получаем AddressablesService и очищаем его
            var addressablesService = Container.Resolve<IAddressablesService>();
            if (addressablesService is AddressablesService service)
            {
                service.Clear();
            }
        }
    }
}