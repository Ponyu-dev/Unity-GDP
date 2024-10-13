using _RTS.Scripts.ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;
using VContainer;

namespace _RTS.Scripts.ECS.Systems
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
            var filter = _world.Filter<PositionComponent>().Inc<PrefabComponent>().End();

            foreach (var entity in filter)
            {
                ref var position = ref _world.GetPool<PositionComponent>().Get(entity);
                ref var prefab = ref _world.GetPool<PrefabComponent>().Get(entity);

                prefab.Prefab.transform.position = position.Position; // Обновляем позицию префаба
            }
        }
    }
}