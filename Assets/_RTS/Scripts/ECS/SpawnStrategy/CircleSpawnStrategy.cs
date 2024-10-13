using _RTS.Scripts.ECS.SpawnStrategy.Base;
using _RTS.Scripts.ECS.Utils;
using Leopotam.EcsLite;
using UnityEngine;

namespace _RTS.Scripts.ECS.SpawnStrategy
{
    public class CircleSpawnStrategy : BaseSpawnStrategy
    {
        public override void SpawnArmy(EcsWorld world, Transform container, int count, Team team, GameObject prefab,
            float spacing)
        {
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
                    var position = center + new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
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