using UnityEngine;
using Utils;

namespace ShootEmUp
{
    public sealed class EnemySpawner :
        ITimerGameListener,
        IStartGameListener,
        IFixedUpdateGameListener,
        IFinishGameListener
    {
        private readonly PoolMono<Enemy> m_PoolMono;
       
        private readonly int m_InitialCount;
        private readonly EnemyPositions m_EnemyPositions;
        private readonly IBulletSpawner m_BulletSpawner;

        private readonly IHitPointsComponent m_HitPointsComponentTarget;
        private readonly ICharacter m_Character;
        
        public EnemySpawner(
            Enemy enemyPrefab, 
            int initialCount, 
            bool autoExpand, 
            Transform container, 
            Transform worldTransform, 
            EnemyPositions enemyPositions,
            IBulletSpawner bulletSpawner,
            ICharacter character,
            IHitPointsComponent hitPointsComponentTarget)
        {
            Debug.Log("[EnemySpawner] constructor");
            m_InitialCount = initialCount;
            m_EnemyPositions = enemyPositions;
            m_BulletSpawner = bulletSpawner;
            m_HitPointsComponentTarget = hitPointsComponentTarget;
            m_Character = character;
            m_PoolMono = new PoolMono<Enemy>(enemyPrefab, container, worldTransform, autoExpand);
        }
        
        //Когда нажали на Start. И идет отсчет. Создаем пул.
        public void OnStartTimer()
        {
            m_PoolMono.CreatePool(m_InitialCount);
        }

        public void OnStartGame()
        {
            for(var i = 0; i < m_InitialCount; i++)
            {
                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            if (!m_PoolMono.TryGet(out var enemy)) return;
            
            var spawnPosition = this.m_EnemyPositions.RandomSpawnPosition();
            var attackPosition = this.m_EnemyPositions.RandomAttackPosition();

            enemy.Construct(
                spawnPosition.position,
                attackPosition.position,
                m_Character.GetTransform(),
                m_HitPointsComponentTarget,
                m_BulletSpawner);
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
            for (var i = 0; i < m_InitialCount; i++)
            {
                var enemy = m_PoolMono.TryGetActive(i);
                if (enemy != null) 
                    enemy.OnFixedUpdate(deltaTime);
            }
        }

        //При финише игры. Очищаем пул.
        public void OnFinishGame()
        {
            m_PoolMono.ClearPool();
        }
    }
}