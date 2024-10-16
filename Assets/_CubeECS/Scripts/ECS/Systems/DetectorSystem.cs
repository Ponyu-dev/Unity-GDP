using CubeECS.Scripts.ECS.Components;
using Leopotam.EcsLite;
using Sirenix.Utilities;
using UnityEngine;
using VContainer;

namespace CubeECS.Scripts.ECS.Systems
{
    public class DetectorSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;

        [Inject]
        public DetectorSystem()
        {
            Debug.Log("DetectorSystem Constructor");
        }
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
        }
        
        private Collider[] GetEnemyInRange(Vector3 position, float range, string layerMask)
        {
            // Получаем все коллайдеры в радиусе, которые находятся на слое врагов
            return Physics.OverlapSphere(position, range, LayerMask.GetMask(layerMask));
        }

        public void Run(IEcsSystems systems)
        {
            var filterEnemy = _world.Filter<DetectorComponent>().Inc<TransformComponent>().End();
            var isMoving = true;
            
            foreach (var entity in filterEnemy)
            {
                ref var transformComponent = ref _world.GetPool<TransformComponent>().Get(entity);
                var position = transformComponent.Value.position;
                ref var detectorComponent = ref _world.GetPool<DetectorComponent>().Get(entity);
                ref var shotComponent = ref _world.GetPool<ShotComponent>().Get(entity);
                
                shotComponent.CollidersEnemy.Clear();
                var colliders = GetEnemyInRange(position, detectorComponent.Range, detectorComponent.LayerDetect);
                if (shotComponent.CollidersEnemy.IsNullOrEmpty()) 
                    shotComponent.CollidersEnemy.AddRange(colliders);
                
                var colliderLength = colliders.Length;

                if (colliderLength <= 1) continue;
                isMoving = false;
            }
            
            var filterArmy = _world.Filter<PositionComponent>().Inc<MovementComponent>().End();
            foreach (var entity in filterArmy)
            {
                ref var movementComponent = ref _world.GetPool<MovementComponent>().Get(entity);
                movementComponent.IsMoving = isMoving;
            }
        }
    }
}