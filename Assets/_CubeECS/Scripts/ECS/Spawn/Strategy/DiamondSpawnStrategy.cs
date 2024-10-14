using CubeECS.Scripts.ECS.Spawn.Base;
using CubeECS.Scripts.ECS.Utils;
using Leopotam.EcsLite;
using UnityEngine;

namespace CubeECS.Scripts.ECS.Spawn.Strategy
{
    public class DiamondSpawnStrategy : BaseSpawn
    {
        public override void SpawnArmy(
            EcsWorld world, Transform container, Vector3 targetPosition,
            int count, Team team, GameObject prefab, float spacing)
        {
            base.SpawnArmy(world, container, targetPosition, count, team, prefab, spacing);
            
            var center = container.position;

            // Вычисляем максимальную ширину ромба
            var diamondWidth = Mathf.CeilToInt((Mathf.Sqrt(1 + 8 * count) - 1) / 2) / 2;

            var totalEntities = 0;

            // Проходим по уровням высоты ромба
            for (var y = -diamondWidth; y <= diamondWidth; y++) 
            {
                // Определяем максимальное количество сущностей по X на уровне Y
                var xLimit = diamondWidth - Mathf.Abs(y); 

                for (var x = -xLimit; x <= xLimit; x++) 
                {
                    // Проверка на превышение количества
                    if (totalEntities >= count) return;

                    // Вычисляем позицию для сущности
                    var position = center + new Vector3(x * spacing, 0, y * spacing); 
                    CreateEntity(world, container, position, team, prefab);
                    totalEntities++; // Увеличиваем общее количество созданных сущностей
                }
            }
        }
    }
}