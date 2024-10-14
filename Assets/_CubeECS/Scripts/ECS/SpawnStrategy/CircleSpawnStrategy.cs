using CubeECS.Scripts.ECS.SpawnStrategy.Base;
using CubeECS.Scripts.ECS.Utils;
using Leopotam.EcsLite;
using UnityEngine;

namespace CubeECS.Scripts.ECS.SpawnStrategy
{
    public class CircleSpawnStrategy : BaseSpawnStrategy
    {
        private Vector3 Circle(float angle, float radius)
        {
            return new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
        }
        
        public override void SpawnArmy(
            EcsWorld world,
            Transform container,
            Vector3 targetPosition,
            int count,
            Team team,
            GameObject prefab,
            float spacing)
        {
            base.SpawnArmy(world, container, targetPosition, count, team, prefab, spacing);
            var center = container.position;
            
            // Общее количество сущностей на каждом радиусе
            var radiusCount = Mathf.CeilToInt(Mathf.Sqrt(count));
            // Параметр для отслеживания общего количества сущностей, добавленных до текущего уровня
            var totalSpawned = 0;

            for (var r = 0; r < radiusCount; r++) // Уровень радиуса
            {
                var radius = r * spacing; // Определяем радиус для текущего уровня
                var entitiesOnThisLevel = Mathf.Min(count - totalSpawned, 6 * r); // Максимум 6 сущностей на уровень

                // Распределяем сущности по кругу на текущем радиусе
                for (var i = 0; i < entitiesOnThisLevel; i++)
                {
                    // Угол для текущей сущности
                    var angle = i * (360f / entitiesOnThisLevel) * Mathf.Deg2Rad;
                    // Вычисляем позицию
                    var position = center + Circle(angle, radius);
                    // Создаем сущность в рассчитанной позиции
                    CreateEntity(world, container, position, team, prefab);
                }

                // Увеличиваем общее количество созданных сущностей
                totalSpawned += entitiesOnThisLevel;
                // Прерываем, если все сущности созданы
                if (totalSpawned >= count)
                    break;
            }
        }
    }
}