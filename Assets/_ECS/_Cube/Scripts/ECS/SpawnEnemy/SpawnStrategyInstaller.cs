using CubeECS.Scripts.ECS.Spawn.Base;
using CubeECS.Scripts.ECS.Spawn.Strategy;
using VContainer;

namespace CubeECS.Scripts.ECS.Spawn
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