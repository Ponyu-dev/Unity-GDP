using CubeECS.Scripts.ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;
using VContainer;

namespace CubeECS.Scripts.ECS.Systems
{
    public class RenderSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;

        [Inject]
        public RenderSystem()
        {
            Debug.Log("RenderSystem Constructor");
        }
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
        }

        public void Run(IEcsSystems systems)
        {
            Debug.Log("RenderSystem Run");
            var filter = _world.Filter<PositionComponent>().Inc<TransformComponent>().End();

            foreach (var entity in filter)
            {
                ref var position = ref _world.GetPool<PositionComponent>().Get(entity);
                ref var prefab = ref _world.GetPool<TransformComponent>().Get(entity);

                prefab.Value.position = position.Position; // Обновляем позицию префаба
            }
        }
    }
}