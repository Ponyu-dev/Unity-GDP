using _RTS.Scripts.ECS.Components;
using _RTS.Scripts.ECS.Utils;
using Leopotam.EcsLite;
using UnityEngine;

namespace _RTS.Scripts.ECS.SpawnStrategy.Base
{
    public abstract class BaseSpawnStrategy : ISpawnStrategy
    {
        protected void CreateEntity(EcsWorld world, Transform container, Vector3 position, Team team, GameObject prefab)
        {
            var entity = world.NewEntity();
            ref var positionComponent = ref world.GetPool<PositionComponent>().Add(entity);
            ref var teamComponent = ref world.GetPool<TeamComponent>().Add(entity);
            ref var prefabComponent = ref world.GetPool<PrefabComponent>().Add(entity);

            positionComponent.Position = position;
            teamComponent.team = team;
            prefabComponent.Prefab = Object.Instantiate(prefab, position, Quaternion.identity, container);
        }

        public abstract void SpawnArmy(EcsWorld world, Transform container, int count, Team team, GameObject prefab,
            float spacing);
    }
}