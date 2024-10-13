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
            positionComponent.Position = position;
            
            ref var teamComponent = ref world.GetPool<TeamComponent>().Add(entity);
            teamComponent.team = team;
            
            ref var movement = ref world.GetPool<MovementComponent>().Add(entity);
            movement.TargetPosition = targetPosition;
            movement.IsMoving = true;
            
            ref var prefabComponent = ref world.GetPool<PrefabComponent>().Add(entity);
            prefabComponent.Prefab = Object.Instantiate(prefab, position, Quaternion.identity, container);
        }

        public abstract void SpawnArmy(EcsWorld world, Transform container, Vector3 targetPosition, int count, Team team, GameObject prefab,
            float spacing);
    }
}