using _ShootEmUp.UI.Scripts;
using VContainer;
using VContainer.Unity;

namespace _ShootEmUp.Scripts.VContainer
{
    public class UILifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<TimerScreen>().AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<StartScreen>().AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<PlayingScreen>().AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<PauseScreen>().AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<FinishScreen>().AsImplementedInterfaces();
        }
    }
}
