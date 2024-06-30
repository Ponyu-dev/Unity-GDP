using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPool : MonoBehaviour
    {
        [SerializeField]
        private int initialCount = 7;
        
        [Header("Spawn")]
        [SerializeField]
        private EnemyPositions enemyPositions;

        [SerializeField]
        private GameObject character;

        [SerializeField]
        private Transform worldTransform;

        [Header("Pool")]
        [SerializeField] private Transform container;

        [SerializeField]
        private GameObject prefab;
        
        [SerializeField]
        private BulletSystem _bulletSystem;

        private readonly Queue<GameObject> enemyPool = new();
        private readonly HashSet<GameObject> m_activeEnemies = new();

        private void Awake()
        {
            for (var i = 0; i < initialCount; i++)
            {
                var enemy = Instantiate(this.prefab, this.container);
                this.enemyPool.Enqueue(enemy);
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
            Debug.Log("SpawnEnemy");
            if (!this.enemyPool.TryDequeue(out var enemy))
            {
                return;
            }

            enemy.transform.SetParent(this.worldTransform);

            var spawnPosition = this.enemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;

            var attackPosition = this.enemyPositions.RandomAttackPosition();
            enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);

            enemy.GetComponent<EnemyAttackAgent>().SetTarget(this.character);

            if (this.m_activeEnemies.Add(enemy))
            {
                enemy.GetComponent<HitPointsComponent>().OnDeath += this.OnDestroyed;
                enemy.GetComponent<EnemyAttackAgent>().OnFireAction += this.OnFireAction;
            }
        }

        private void OnDestroyed(GameObject enemy)
        {
            if (m_activeEnemies.Remove(enemy))
            {
                enemy.GetComponent<HitPointsComponent>().OnDeath -= this.OnDestroyed;
                enemy.GetComponent<EnemyAttackAgent>().OnFireAction -= this.OnFireAction;

                UnspawnEnemy(enemy);
            }
        }

        private void UnspawnEnemy(GameObject enemy)
        {
            enemy.transform.SetParent(this.container);
            this.enemyPool.Enqueue(enemy);
            SpawnEnemy();
        }

        private void OnFireAction(Args args)
        {
            _bulletSystem.FlyBulletByArgs(args);
        }
    }
}