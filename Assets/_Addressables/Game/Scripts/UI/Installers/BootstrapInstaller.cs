using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace SampleGame
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private AssetReference menuScreenReference;
        [SerializeField] private AssetReference pauseScreenReference;

        public override void InstallBindings()
        {
            Container.BindInstance(menuScreenReference).WithId("MenuScreen").AsCached().NonLazy();
            Container.BindInstance(pauseScreenReference).WithId("PauseScreen").AsCached().NonLazy();
            Container.BindInterfacesTo<UIAssetsService>().AsSingle().NonLazy();
        }
    }
}