using _ShootEmUp.UI.Scripts;
using ShootEmUp;
using VContainer;
using VContainer.Unity;

namespace _ShootEmUp.Scripts.VContainer
{
    public class SceneLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<GameManager>().As<IGameManager>();

            ConfigureUIListeners(builder);
        }

        private void ConfigureUIListeners(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<TimerScreen>().AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<StartScreen>().AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<PlayingScreen>().AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<PauseScreen>().AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<FinishScreen>().AsImplementedInterfaces();
        }
    }
}
