using CubeECS.Scripts.ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;
using VContainer;

namespace CubeECS.Scripts.ECS.Systems
{
    public class DetectorSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private readonly float _moveSpeed = 1f;

        [Inject]
        public DetectorSystem()
        {
            Debug.Log("DetectorSystem Constructor");
        }
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<ArmyPositionComponent>().Inc<ArmyMovementComponent>().End();
        }
        
        private bool IsEnemyInRange(Vector3 position, float range, string layerMask)
        {
            // Получаем все коллайдеры в радиусе, которые находятся на слое врагов
            var size = Physics.OverlapSphere(position, range, LayerMask.GetMask(layerMask)).Length;

            // Проверяем, есть ли хотя бы один враг
            Debug.Log($"IsEnemyInRange {layerMask} size = {size}");
            
            return size <= 0;
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var position = ref _world.GetPool<ArmyPositionComponent>().Get(entity);
                ref var movementComponent = ref _world.GetPool<ArmyMovementComponent>().Get(entity);
                ref var armyDetectorComponent = ref _world.GetPool<ArmyDetectorComponent>().Get(entity);

                movementComponent.IsMoving = IsEnemyInRange(position.Value, armyDetectorComponent.Range, armyDetectorComponent.LayerDetect);
            }
        }
    }
}