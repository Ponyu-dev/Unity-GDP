using System;
using _RTS.Scripts.ECS.Systems;
using Leopotam.EcsLite;
using VContainer;
using VContainer.Unity;

namespace _RTS.Scripts.ECS.Systems
{
    public class GameRunner : IInitializable, IDisposable, ITickable
    {
        private readonly EcsWorld _world;
        private readonly EcsSystems _systems;

        [Inject]
        public GameRunner(
            MovementSystem movementSystem,
            RenderSystem renderSystem,
            SpawnSystem spawnSystem)
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            _systems
                .Add(spawnSystem)
                .Add(renderSystem)
                .Add(movementSystem);
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