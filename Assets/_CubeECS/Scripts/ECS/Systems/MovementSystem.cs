using CubeECS.Scripts.ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;
using VContainer;

namespace CubeECS.Scripts.ECS.Systems
{
    public class MovementSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter _filter;
        private readonly float _moveSpeed = 1f;

        [Inject]
        public MovementSystem()
        {
            Debug.Log("MovementSystem Constructor");
        }
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<PositionComponent>().Inc<MovementComponent>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var movement = ref _world.GetPool<MovementComponent>().Get(entity);

                if (!movement.IsMoving) continue;
                
                ref var position = ref _world.GetPool<PositionComponent>().Get(entity);

                // Рассчитываем вектор направления к целевой позиции
                var direction = (movement.TargetPosition - position.Value).normalized;
                var distanceToTarget = Vector3.Distance(position.Value, movement.TargetPosition);

                // Если объект находится дальше 0.1f от целевой позиции, продолжаем движение
                if (distanceToTarget > 0.1f)
                {
                    // Определяем, насколько перемещаемся в этом кадре
                    var moveStep = _moveSpeed * Time.deltaTime;

                    // Если оставшееся расстояние меньше, чем шаг, останавливаем на целевой позиции
                    if (distanceToTarget < moveStep)
                    {
                        position.Value = movement.TargetPosition; // Устанавливаем на целевую позицию
                    }
                    else
                    {
                        position.Value += direction * moveStep; // Обновляем позицию
                    }
                }
                else
                {
                    // Объект достиг целевой позиции, фиксируем его
                    position.Value = movement.TargetPosition;
                    movement.IsMoving = false;
                }
            }
        }
    }
}