using CubeECS.Scripts.ECS.Components;
using CubeECS.Scripts.ECS.Utils;
using Leopotam.EcsLite;
using UnityEngine;

namespace CubeECS.Scripts.ECS.SpawnStrategy.Base
{
    public abstract class BaseSpawnStrategy : ISpawnStrategy
    {
        protected void CreateEntity(EcsWorld world, Transform container, Vector3 position, Vector3 targetPosition, Team team, GameObject prefab)
        {
            var entity = world.NewEntity();
            ref var positionComponent = ref world.GetPool<PositionComponent>().Add(entity);
            ref var teamComponent = ref world.GetPool<TeamComponent>().Add(entity);
            ref var prefabComponent = ref world.GetPool<PrefabComponent>().Add(entity);
            ref var movement = ref world.GetPool<MovementComponent>().Add(entity);

            movement.TargetPosition = new Vector3(targetPosition.x, 0, position.z);
            Debug.Log($"BaseSpawnStrategy CreateEntity team {team} targetPosition = {movement.TargetPosition}");
            
            positionComponent.Position = position;
            teamComponent.team = team;
            prefabComponent.Prefab = Object.Instantiate(prefab, position, Quaternion.identity, container);
        }

        public abstract void SpawnArmy(EcsWorld world, Transform container, Vector3 targetPosition, int count, Team team, GameObject prefab,
            float spacing);
    }
}