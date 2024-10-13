using _RTS.Scripts.ECS.SpawnStrategy.Base;
using _RTS.Scripts.ECS.Utils;
using Leopotam.EcsLite;
using UnityEngine;

namespace _RTS.Scripts.ECS.SpawnStrategy
{
    public class TriangleSpawnStrategy : BaseSpawnStrategy
    {
        private Vector3 Triangle(int currentCount, int currentRow, float spacing)
        {
            return new Vector3((currentCount - currentRow / 2f) * spacing, 0, currentRow * spacing);
        }
        
        public override void SpawnArmy(EcsWorld world, Transform container, Vector3 targetPosition, int count, Team team, GameObject prefab,
            float spacing)
        {
            var center = container.position;
            var currentRow = 0;
            var currentCount = 0;

            for (var i = 0; i < count; i++)
            {
                if (currentCount >= currentRow + 1)
                {
                    currentRow++;
                    currentCount = 0;
                }

                var position = center + Triangle(currentCount, currentRow, spacing);
                var target = targetPosition + Triangle(currentCount, currentRow, spacing);
                CreateEntity(world, container, position, target, team, prefab);
                currentCount++;
            }
        }
    }
}