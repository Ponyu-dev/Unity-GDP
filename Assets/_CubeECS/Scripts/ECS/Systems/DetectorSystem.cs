using CubeECS.Scripts.ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;
using VContainer;
using Random = System.Random;

namespace CubeECS.Scripts.ECS.Systems
{
    public class DetectorSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private readonly Random _random = new();

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

                var collider = GetEnemyInRange(
                    position,
                    detectorComponent.Range,
                    detectorComponent.LayerDetect);
                var colliderLength = collider.Length;

                if (colliderLength <= 0) continue;
                
                var targetPosition = collider[_random.Next(colliderLength)].transform.position;
                var direction = targetPosition - position;
                direction.Normalize();

                shotComponent.Direction = direction;
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