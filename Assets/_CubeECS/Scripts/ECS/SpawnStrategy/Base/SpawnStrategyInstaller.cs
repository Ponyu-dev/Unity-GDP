using VContainer;

namespace CubeECS.Scripts.ECS.SpawnStrategy.Base
{
    public class SpawnStrategyInstaller
    {
        public void Configure(IContainerBuilder builder)
        {
            builder.Register<ISpawnStrategy, SquareSpawnStrategy>(Lifetime.Singleton);
            builder.Register<ISpawnStrategy, TriangleSpawnStrategy>(Lifetime.Singleton);
            builder.Register<ISpawnStrategy, CircleSpawnStrategy>(Lifetime.Singleton);
            builder.Register<ISpawnStrategy, DiamondSpawnStrategy>(Lifetime.Singleton);
        }
    }
}