using CubeECS.Scripts.ECS.Spawn.Base;
using Leopotam.EcsLite;
using CubeECS.Scripts.ECS.Utils;
using UnityEngine;

namespace CubeECS.Scripts.ECS.Spawn.Strategy
{
    public class SquareSpawnStrategy : BaseSpawn
    {
        private Vector3 Square(int i, int columns, int rows, float spacing)
        {
            return new Vector3(
                (i % columns) * spacing - (columns - 1) * spacing * 0.5f,
                0,
                (i / columns) * spacing - (rows - 1) * spacing * 0.5f);
        }
        
        public override void SpawnArmy(EcsWorld world, Transform container, Vector3 targetPosition, int count, Team team, GameObject prefab,
            float spacing)
        {
            base.SpawnArmy(world, container, targetPosition, count, team, prefab, spacing);
            var center = container.position;
            var rows = Mathf.CeilToInt(Mathf.Sqrt(count));
            var columns = Mathf.CeilToInt((float)count / rows);

            for (var i = 0; i < count; i++)
            {
                var position = center + Square(i, columns, rows, spacing);
                CreateEntity(world, container, position, team, prefab);
            }
        }
    }
}