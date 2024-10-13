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
            var radius = Mathf.Sqrt(count) * spacing * 0.5f;

            for (var i = 0; i < count; i++)
            {
                var angle = i * Mathf.PI * 2 / count;
                var position = center + new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
                CreateEntity(world, container, position, team, prefab);
            }
        }
    }
}