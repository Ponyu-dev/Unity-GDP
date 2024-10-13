using _RTS.Scripts.ECS.SpawnStrategy.Base;
using Leopotam.EcsLite;
using _RTS.Scripts.ECS.Utils;
using UnityEngine;

namespace _RTS.Scripts.ECS.SpawnStrategy
{
    public class SquareSpawnStrategy : BaseSpawnStrategy
    {
        public override void SpawnArmy(EcsWorld world, Transform container, int count, Team team, GameObject prefab,
            float spacing)
        {
            var center = container.position;
            var rows = Mathf.CeilToInt(Mathf.Sqrt(count));
            var columns = Mathf.CeilToInt((float)count / rows);

            for (var i = 0; i < count; i++)
            {
                var position = center + new Vector3((i % columns) * spacing - (columns - 1) * spacing * 0.5f, 0,
                    (i / columns) * spacing - (rows - 1) * spacing * 0.5f);
                CreateEntity(world, container, position, team, prefab);
            }
        }
    }
}