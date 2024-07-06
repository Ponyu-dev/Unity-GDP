using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPool : MonoBehaviour
    {
        [SerializeField] private BulletSystem bulletSystem;
        
        [Header("Spawn")]
        [SerializeField] private EnemyPositions enemyPositions;
        [SerializeField] private GameObject character;
        [SerializeField] private Transform worldTransform;
        
        [Header("Pool")]
        [SerializeField] private Transform container;
        [SerializeField] private EnemyController prefab;
        [SerializeField] private int initialCount = 7;

        private readonly Queue<EnemyController> m_EnemyPool = new();
        private readonly HashSet<EnemyController> m_ActiveEnemies = new();

        private void Awake()
        {
            for (var i = 0; i < initialCount; i++)
            {
                var enemy = Instantiate(this.prefab, this.container);
                this.m_EnemyPool.Enqueue(enemy);
            }
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
            if (!this.m_EnemyPool.TryDequeue(out var enemy))
            {
                return;
            }
            
            var spawnPosition = this.enemyPositions.RandomSpawnPosition();
            var attackPosition = this.enemyPositions.RandomAttackPosition();

            enemy.Construct(
                worldTransform,
                spawnPosition.position,
                attackPosition.position,
                character.transform,
                character.GetComponent<HitPointsComponent>(),
                bulletSystem);
            enemy.OnDestroyed += OnDestroyed;
            this.m_ActiveEnemies.Add(enemy);
        }

        private void OnDestroyed(EnemyController enemy)
        {
            if (m_ActiveEnemies.Remove(enemy))
            {
                enemy.OnDestroyed -= OnDestroyed;
                UnSpawnEnemy(enemy);
            }
        }

        private void UnSpawnEnemy(EnemyController enemy)
        {
            enemy.transform.SetParent(this.container);
            this.m_EnemyPool.Enqueue(enemy);
            SpawnEnemy();
        }
    }
}