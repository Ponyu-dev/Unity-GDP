using System;
using Leopotam.EcsLite;
using VContainer;
using VContainer.Unity;

namespace CubeECS.Scripts.ECS.Systems
{
    public class GameRunner : IInitializable, IDisposable, ITickable
    {
        private readonly EcsWorld _world;
        private readonly EcsSystems _systems;

        [Inject]
        public GameRunner(
            EcsWorld world,
            MovementSystem movementSystem,
            RenderSystem renderSystem,
            DetectorSystem detectorSystem,
            ShotSystem shotSystem,
            BulletCollisionSystem bulletCollisionSystem,
            SpawnEnemySystem spawnSystem)
        {
            _world = world;
            _systems = new EcsSystems(_world);
            _systems
                .Add(spawnSystem)
                .Add(detectorSystem)
                .Add(shotSystem)
                .Add(movementSystem)
                .Add(bulletCollisionSystem)
                .Add(renderSystem);
        }
        
        public void Initialize()
        {
            _systems.Init();
        }
        
        public void Tick()
        {
            // Выполняем все подключенные системы.
            _systems?.Run();
        }

        public void Dispose()
        {
            // Уничтожаем подключенные системы.
            _systems?.Destroy ();
            
            // Очищаем окружение.
            _world?.Destroy ();
        }
    }
}