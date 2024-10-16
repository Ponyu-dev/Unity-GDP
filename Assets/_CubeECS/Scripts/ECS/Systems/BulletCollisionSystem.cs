using _CubeECS.Scripts.ECS.Events;
using CubeECS.Scripts.ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;
using VContainer;

namespace CubeECS.Scripts.ECS.Systems
{
    public class BulletCollisionSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        
        [Inject]
        public BulletCollisionSystem()
        {
            Debug.Log("BulletCollisionSystem Constructor");
        }
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
        }

        public void Run(IEcsSystems systems)
        {
            var hitEventPool = _world.GetPool<BulletHitEvent>();
            var healthPool = _world.GetPool<HealthComponent>();
            var bulletPool = _world.GetPool<BulletComponent>();

            foreach (var eventEntity in _world.Filter<BulletHitEvent>().End())
            {
                ref var hitEvent = ref hitEventPool.Get(eventEntity);

                // Проверяем, что цель имеет компонент здоровья
                if (healthPool.Has(hitEvent.TargetEntity))
                {
                    ref var targetHealth = ref healthPool.Get(hitEvent.TargetEntity);
                    targetHealth.Value -= 1;

                    // Если здоровье достигло 0, уничтожаем юнит
                    // Перенести в DestroySystem
                    if (targetHealth.Value <= 0)
                    {
                        _world.DelEntity(hitEvent.TargetEntity);
                        Object.Destroy(hitEvent.TargetGO);
                    }
                }
                else
                {
                    _world.DelEntity(hitEvent.TargetEntity);
                    Object.Destroy(hitEvent.TargetGO);
                }

                // Уничтожаем пулю
                if (bulletPool.Has(hitEvent.BulletEntity))
                {
                    _world.DelEntity(hitEvent.BulletEntity);
                    Object.Destroy(hitEvent.BulletGO);
                }

                // Удаляем событие после обработки
                _world.DelEntity(eventEntity);
            }
        }
    }
}