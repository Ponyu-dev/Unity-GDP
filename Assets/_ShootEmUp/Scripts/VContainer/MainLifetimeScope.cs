using ShootEmUp;
using VContainer;
using VContainer.Unity;

namespace _ShootEmUp.Scripts.VContainer
{
    public class MainLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<GameManager>().AsImplementedInterfaces();
            
            builder.Register<ShootInput>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<MoveInput>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}