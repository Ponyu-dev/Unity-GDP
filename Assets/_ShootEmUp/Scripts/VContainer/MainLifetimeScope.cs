using ShootEmUp;
using VContainer;
using VContainer.Unity;

namespace _ShootEmUp.Scripts.VContainer
{
    public class MainLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<GameManager>().As<IGameManager>();
        }
    }
}