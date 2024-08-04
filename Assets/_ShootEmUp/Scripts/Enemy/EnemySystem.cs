using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace ShootEmUp
{
    public sealed class EnemySystem : MonoBehaviour, IGameFixedUpdateListener
    {
        [FormerlySerializedAs("bulletSystem")] [SerializeField] private BulletSpawner bulletSpawner;
        
        [Header("Spawn")]
        [SerializeField] private EnemyPositions enemyPositions;
        [SerializeField] private GameObject character;
        [SerializeField] private Transform worldTransform;
        
        [Header("Pool")]
        [SerializeField] private Transform container;
        [SerializeField] private Enemy prefab;
        [SerializeField] private int initialCount = 7;
        [SerializeField] private bool autoExpand = false;

        private PoolMono<Enemy> m_PoolMono;

        private void Awake()
        {
            m_PoolMono = new PoolMono<Enemy>(prefab, initialCount, container, worldTransform, autoExpand);
        }

        private void Start()
        {
            for(var i = 0; i < initialCount; i++)
            {
                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            if (!m_PoolMono.TryGet(out var enemy)) return;
            
            var spawnPosition = this.enemyPositions.RandomSpawnPosition();
            var attackPosition = this.enemyPositions.RandomAttackPosition();

            enemy.Construct(
                spawnPosition.position,
                attackPosition.position,
                character.transform,
                character.GetComponent<HitPointsComponent>(),
                bulletSpawner);
            enemy.OnDeathbed += OnDestroyed;
        }

        private void OnDestroyed(Enemy enemy)
        {
            enemy.OnDeathbed -= OnDestroyed;
            m_PoolMono.InactiveObject(enemy);
            SpawnEnemy();
        }

        public void OnFixedUpdate(float deltaTime)
        {
            for (var i = 0; i < initialCount; i++)
            {
                var enemy = m_PoolMono.TryGetActive(i);
                if (enemy != null) 
                    enemy.OnFixedUpdate(deltaTime);
            }
        }
    }
}