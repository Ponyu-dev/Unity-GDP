using _RTS.Scripts.ECS.SpawnStrategy.Base;
using _RTS.Scripts.ECS.Utils;
using Leopotam.EcsLite;
using UnityEngine;

namespace _RTS.Scripts.ECS.SpawnStrategy
{
    public class DiamondSpawnStrategy : BaseSpawnStrategy
    {
        public override void SpawnArmy(EcsWorld world, Transform container, int count, Team team, GameObject prefab,
            float spacing)
        {
            var center = container.position;
            for (var i = 0; i < count; i++)
            {
                var diamondLayer = (int)Mathf.Sqrt(i);
                var indexInLayer = i - (diamondLayer * diamondLayer);

                var position = center + new Vector3((indexInLayer - diamondLayer) * spacing, 0, diamondLayer * spacing);
                CreateEntity(world, container, position, team, prefab);
            }
        }
    }
}