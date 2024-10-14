using CubeECS.Scripts.ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;
using VContainer;

namespace CubeECS.Scripts.ECS.Systems
{
    public class RenderSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;

        [Inject]
        public RenderSystem()
        {
            Debug.Log("RenderSystem Constructor");
        }
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<ArmyPositionComponent>().Inc<ArmyTransformComponent>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var position = ref _world.GetPool<ArmyPositionComponent>().Get(entity);
                ref var prefab = ref _world.GetPool<ArmyTransformComponent>().Get(entity);

                prefab.Value.position = position.Value; // Обновляем позицию
            }
        }
    }
}