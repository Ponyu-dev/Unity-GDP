using CubeECS.Scripts.ECS.Components;
using CubeECS.Scripts.ECS.Utils;
using Leopotam.EcsLite;
using UnityEngine;

namespace CubeECS.Scripts.ECS.Spawn.Base
{
    public abstract class BaseSpawn : ISpawnStrategy
    {
        private void CreateArmy(EcsWorld world, Transform container, Team team, Vector3 targetPosition)
        {
            var entity = world.NewEntity();
            
            ref var teamComponent = ref world.GetPool<TeamComponent>().Add(entity);
            teamComponent.team = team;
            
            ref var transform = ref world.GetPool<TransformComponent>().Add(entity);
            transform.Value = container;
            
            ref var positionComponent = ref world.GetPool<PositionComponent>().Add(entity);
            positionComponent.Position = container.position;
            
            ref var movement = ref world.GetPool<MovementComponent>().Add(entity);
            movement.TargetPosition = targetPosition;
            movement.IsMoving = true;
        }
        
        protected void CreateEntity(
            EcsWorld world, Transform container, Vector3 position, Team team, GameObject prefab)
        {
            var entity = world.NewEntity();
            
            ref var teamComponent = ref world.GetPool<TeamComponent>().Add(entity);
            teamComponent.team = team;
            
            ref var prefabComponent = ref world.GetPool<PrefabComponent>().Add(entity);
            prefabComponent.Prefab = Object.Instantiate(prefab, position, Quaternion.identity, container);
        }

        public virtual void SpawnArmy(
            EcsWorld world, Transform container, Vector3 targetPosition,
            int count, Team team, GameObject prefab, float spacing)
        {
            CreateArmy(world, container, team, targetPosition);
        }
    }
}