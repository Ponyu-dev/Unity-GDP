using System.Collections;
using UnityEngine;
using Utils;

namespace ShootEmUp
{
    public sealed class EnemySystem : MonoBehaviour
    {
        [SerializeField] private BulletSystem bulletSystem;
        
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

        private IEnumerator Start()
        {
            for(var i = 0; i < initialCount; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(1);
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
                bulletSystem);
            enemy.OnDeathbed += OnDestroyed;
        }

        private void OnDestroyed(Enemy enemy)
        {
            enemy.OnDeathbed -= OnDestroyed;
            m_PoolMono.InactiveObject(enemy);
            SpawnEnemy();
        }
    }
}