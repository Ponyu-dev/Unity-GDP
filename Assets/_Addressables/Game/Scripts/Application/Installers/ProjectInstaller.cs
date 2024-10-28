using UnityEngine;
using Zenject;

namespace SampleGame
{
    [CreateAssetMenu(
        fileName = "ProjectInstaller",
        menuName = "Installers/New ProjectInstaller"
    )]
    public sealed class ProjectInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            this.Container.BindInterfacesTo<AddressablesService>()
                .AsSingle()
                .NonLazy();
            
            this.Container.BindInterfacesTo<SceneLoader>().AsSingle().NonLazy();
            this.Container.Bind<ApplicationExiter>().AsSingle().NonLazy();
            this.Container.Bind<GameLoader>().AsSingle().NonLazy();
            this.Container.Bind<MenuLoader>().AsSingle().NonLazy();
        }
    }
}