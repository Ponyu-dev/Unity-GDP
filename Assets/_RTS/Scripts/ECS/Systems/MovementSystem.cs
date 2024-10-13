using _RTS.Scripts.ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;
using VContainer;

namespace _RTS.Scripts.ECS.Systems
{
    public class MovementSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private readonly float _moveSpeed = 0.5f;

        [Inject]
        public MovementSystem()
        {
            Debug.Log("MovementSystem Constructor");
        }
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
        }

        public void Run(IEcsSystems systems)
        {
            Debug.Log("MovementSystem Run");
            var filter = _world.Filter<PositionComponent>().Inc<MovementComponent>().End();

            foreach (var entity in filter)
            {
                ref var position = ref _world.GetPool<PositionComponent>().Get(entity);
                ref var movement = ref _world.GetPool<MovementComponent>().Get(entity);

                // Рассчитываем вектор направления к целевой позиции
                var direction = (movement.TargetPosition - position.Position).normalized;
                var distanceToTarget = Vector3.Distance(position.Position, movement.TargetPosition);

                // Если объект находится дальше 0.1f от целевой позиции, продолжаем движение
                if (distanceToTarget > 0.1f)
                {
                    // Определяем, насколько перемещаемся в этом кадре
                    var moveStep = _moveSpeed * Time.deltaTime;

                    // Если оставшееся расстояние меньше, чем шаг, останавливаем на целевой позиции
                    if (distanceToTarget < moveStep)
                    {
                        position.Position = movement.TargetPosition; // Устанавливаем на целевую позицию
                    }
                    else
                    {
                        position.Position += direction * moveStep; // Обновляем позицию
                    }
                }
                else
                {
                    // Объект достиг целевой позиции, фиксируем его
                    position.Position = movement.TargetPosition;
                    // Если хотите, можно установить IsMoving в false, чтобы остановить движение
                }
            }
        }
    }
}