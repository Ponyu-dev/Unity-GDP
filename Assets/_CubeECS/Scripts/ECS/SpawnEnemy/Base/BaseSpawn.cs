using System.Collections.Generic;
using _CubeECS.Scripts.ECS.Utils;
using CubeECS.Scripts.ECS.Components;
using CubeECS.Scripts.ECS.Utils;
using Leopotam.EcsLite;
using UnityEngine;

namespace CubeECS.Scripts.ECS.Spawn.Base
{
    public abstract class BaseSpawn : ISpawnStrategy
    {
        //TODO вынести в конфиги.
        private const float DetectorRange = 3f;
        private const float ShootCooldown = 2f;
        private const float BulletSpeed = 1f;
        private const int DefaultHealth = 1;
        
        private string GetLayerDetect(Team team)
        {
            return team == Team.Red ? $"Cube{Team.Blue.ToString()}" : $"Cube{Team.Red.ToString()}";
        }
        
        protected void CreateArmy(
            EcsWorld world, Transform container, Team team, Vector3 targetPosition)
        {
            var entity = world.NewEntity();
            
            ref var teamComponent = ref world.GetPool<TeamComponent>().Add(entity);
            teamComponent.Value = team;
            
            ref var transform = ref world.GetPool<TransformComponent>().Add(entity);
            transform.Value = container;
            
            ref var positionComponent = ref world.GetPool<PositionComponent>().Add(entity);
            positionComponent.Value = container.position;
            
            ref var movement = ref world.GetPool<MovementComponent>().Add(entity);
            movement.TargetPosition = targetPosition;
            movement.IsMoving = true;
        }
        
        protected void CreateEntity(
            EcsWorld world, Transform container, Vector3 position, Team team, GameObject prefab)
        {
            var entity = world.NewEntity();
            
            ref var teamComponent = ref world.GetPool<TeamComponent>().Add(entity);
            teamComponent.Value = team;
            
            ref var healthComponent = ref world.GetPool<HealthComponent>().Add(entity);
            healthComponent.Value = Random.Range(1, 3);
            
            ref var detectorComponent = ref world.GetPool<DetectorComponent>().Add(entity);
            detectorComponent.Range = DetectorRange;
            detectorComponent.LayerDetect = GetLayerDetect(team);
            
            ref var shotComponent = ref world.GetPool<ShotComponent>().Add(entity);
            shotComponent.Team = team;
            shotComponent.CollidersEnemy = new List<Collider>();
            shotComponent.ShootCooldown = Random.Range(0.5f, 3f);
            shotComponent.BulletSpeed = Random.Range(0.5f, 3f);
            shotComponent.LastShootTime = Time.time;

            ref var prefabComponent = ref world.GetPool<PrefabComponent>().Add(entity);
            prefabComponent.Prefab = Object.Instantiate(prefab, position, Quaternion.identity, container);

            if (prefabComponent.Prefab.TryGetComponent<EnemyCollider>(out var enemyCollider))
                enemyCollider.SetEntityId(entity);
            
            ref var transformComponent = ref world.GetPool<TransformComponent>().Add(entity);
            transformComponent.Value = prefabComponent.Prefab.transform;
        }

        public abstract void SpawnArmy(
            EcsWorld world, Transform container, Vector3 targetPosition,
            int count, Team team, GameObject prefab, float spacing);
    }
}