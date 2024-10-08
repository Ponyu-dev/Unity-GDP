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
        private readonly float m_CountdownFire;
        private readonly IBulletSpawner m_BulletSpawner;
        private readonly BulletConfig m_BulletConfig;
        private readonly ICharacter m_Character;
        private readonly IHitPointsComponent m_HitPointsComponentTarget;

        public EnemySpawner(
            Enemy enemyPrefab, 
            int initialCount, 
            bool autoExpand, 
            float countdownFire,
            Transform container, 
            Transform worldTransform, 
            EnemyPositions enemyPositions,
            BulletConfig bulletConfig,
            IBulletSpawner bulletSpawner,
            ICharacter character,
            IHitPointsComponent hitPointsComponentTarget)
        {
            Debug.Log("[EnemySpawner] constructor");
            m_InitialCount = initialCount;
            m_CountdownFire = countdownFire;
            m_EnemyPositions = enemyPositions;
            m_BulletSpawner = bulletSpawner;
            m_BulletConfig = bulletConfig;
            m_Character = character;
            m_HitPointsComponentTarget = hitPointsComponentTarget;
            
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
                m_CountdownFire,
                m_Character.GetTransform(),
                m_BulletSpawner,
                m_BulletConfig);
            enemy.OnDeath += OnDestroyed;
        }

        private void OnDestroyed(Enemy enemy)
        {
            enemy.OnDeath -= OnDestroyed;
            m_PoolMono.InactiveObject(enemy);
            SpawnEnemy();
        }

        public void OnFixedUpdate(float deltaTime)
        {
            for (var i = 0; i < m_InitialCount; i++)
            {
                var enemy = m_PoolMono.TryGetActive(i);
                if (enemy == null) continue;
                
                enemy.OnMove(deltaTime);

                if (m_HitPointsComponentTarget.IsHitPointsExists())
                    enemy.OnAttack(deltaTime);
            }
        }

        //При финише игры. Очищаем пул.
        public void OnFinishGame()
        {
            m_PoolMono.ClearPool();
        }
    }
}