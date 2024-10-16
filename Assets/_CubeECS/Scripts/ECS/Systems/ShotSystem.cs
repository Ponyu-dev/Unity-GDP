using _CubeECS.Scripts.ECS.Bullets;
using CubeECS.Scripts.ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;
using VContainer;
using Random = System.Random;

namespace CubeECS.Scripts.ECS.Systems
{
    public class ShotSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly Random _random = new();
        private readonly GameObject _bulletPrefab;
        private readonly Transform _container;
        private EcsWorld _world;
        
        [Inject]
        public ShotSystem(GameObject bulletPrefab, Transform container)
        {
            _bulletPrefab = bulletPrefab;
            _container = container;
            Debug.Log("ShotSystem Constructor");
        }
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
        }

        public void Run(IEcsSystems systems)
        {
            var filterEnemy = _world.Filter<ShotComponent>().Inc<TransformComponent>().End();

            foreach (var enemy in filterEnemy)
            {
                ref var shotComponent = ref _world.GetPool<ShotComponent>().Get(enemy);
                ref var teamComponent = ref _world.GetPool<TeamComponent>().Get(enemy);
                
                var collidersEnemy = shotComponent.CollidersEnemy;
                Debug.Log($"[ShotSystem] {teamComponent.Value} {collidersEnemy.Count}");
                if (collidersEnemy.Count <= 0) continue;
                
                if (!(Time.time >= shotComponent.LastShootTime + shotComponent.ShootCooldown)) continue;
                
                Debug.Log($"[ShotSystem] Shoot");
                shotComponent.LastShootTime = Time.time;

                ref var transformComponent = ref _world.GetPool<TransformComponent>().Get(enemy);

                var position = transformComponent.Value.position;
                
                var targetPosition = collidersEnemy[_random.Next(collidersEnemy.Count)].transform.position;
                var shootDirection = (targetPosition - position).normalized;

                // Создаем пулю в ECS
                var bulletEntity = _world.NewEntity();
                ref var bullet = ref _world.GetPool<BulletComponent>().Add(bulletEntity);
                bullet.Team = teamComponent.Value;
                
                var bulletObject = Object.Instantiate(_bulletPrefab, position, Quaternion.identity, _container);
                if (bulletObject.TryGetComponent<BulletCollider>(out var collider))
                {
                    collider.SetEntityId(bulletEntity);
                    collider.SetEcsWorld(_world);
                }

                if (bulletObject.TryGetComponent<Rigidbody>(out var rigidbody))
                    rigidbody.velocity = shootDirection * shotComponent.BulletSpeed;

                shotComponent.CollidersEnemy.Clear();
            }
        }
    }
}