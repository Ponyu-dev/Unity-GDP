using _RTS.Scripts.ECS.Utils;
using Leopotam.EcsLite;
using UnityEngine;

namespace _RTS.Scripts.ECS.SpawnStrategy.Base
{
    public interface ISpawnStrategy
    {
        public void SpawnArmy(EcsWorld world, Transform container, int count, Team team, GameObject prefab, float spacing);
    }
}