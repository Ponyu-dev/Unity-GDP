using _RTS.Scripts.ECS.SpawnStrategy.Base;
using _RTS.Scripts.ECS.Utils;
using Leopotam.EcsLite;
using UnityEngine;

namespace _RTS.Scripts.ECS.SpawnStrategy
{
    public class TriangleSpawnStrategy : BaseSpawnStrategy
    {
        public override void SpawnArmy(EcsWorld world, Transform container, int count, Team team, GameObject prefab,
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

                var position =
                    center + new Vector3((currentCount - currentRow / 2f) * spacing, 0, currentRow * spacing);
                CreateEntity(world, container, position, team, prefab);
                currentCount++;
            }
        }
    }
}