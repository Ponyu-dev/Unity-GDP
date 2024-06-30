using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPool : MonoBehaviour
    {
        [SerializeField]
        private BulletSystem _bulletSystem;
        
        [Header("Spawn")]
        [SerializeField]
        private EnemyPositions _enemyPositions;

        [SerializeField]
        private GameObject _character;

        [SerializeField]
        private Transform _worldTransform;

        [Header("Pool")]
        [SerializeField] private Transform _container;

        [SerializeField]
        private EnemyController _prefab;
        
        [SerializeField]
        private int _initialCount = 7;

        private readonly Queue<EnemyController> _enemyPool = new();
        private readonly HashSet<EnemyController> _activeEnemies = new();

        private void Awake()
        {
            for (var i = 0; i < _initialCount; i++)
            {
                var enemy = Instantiate(this._prefab, this._container);
                this._enemyPool.Enqueue(enemy);
            }
        }

        private IEnumerator Start()
        {
            for(var i = 0; i < _initialCount; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(1);
            }
        }

        private void SpawnEnemy()
        {
            if (!this._enemyPool.TryDequeue(out var enemy))
            {
                return;
            }
            
            var spawnPosition = this._enemyPositions.RandomSpawnPosition();
            var attackPosition = this._enemyPositions.RandomAttackPosition();

            enemy.Construct(
                _worldTransform,
                spawnPosition.position,
                attackPosition.position,
                _character.transform.position,
                _character.GetComponent<HitPointsComponent>(),
                _bulletSystem);
            enemy.OnDestroyed += OnDestroyed;
            this._activeEnemies.Add(enemy);
        }

        private void OnDestroyed(EnemyController enemy)
        {
            if (_activeEnemies.Remove(enemy))
            {
                enemy.OnDestroyed -= OnDestroyed;
                UnspawnEnemy(enemy);
            }
        }

        private void UnspawnEnemy(EnemyController enemy)
        {
            enemy.transform.SetParent(this._container);
            this._enemyPool.Enqueue(enemy);
            SpawnEnemy();
        }
    }
}