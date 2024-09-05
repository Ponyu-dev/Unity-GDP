using Sirenix.Utilities;
using UnityEngine;
using Utils;

namespace ShootEmUp
{
    public interface IBulletSpawner
    { 
        void CreateBullet(BulletData bulletData);
    }
    
    public sealed class BulletSpawner :
        IBulletSpawner,
        IGameTimerListener,
        IGameFixedUpdateListener,
        IGameFinishListener
    {
        private readonly PoolMono<Bullet> m_PoolMono;
        private readonly int m_InitialCount;
        private readonly LevelBounds m_LevelBounds;
        
        public BulletSpawner(
            Bullet bulletPrefab, 
            int initialCount, 
            bool autoExpand, 
            Transform container, 
            Transform worldTransform, 
            LevelBounds levelBounds)
        {
            Debug.Log("[BulletSpawner] constructor");
            m_InitialCount = initialCount;
            m_LevelBounds = levelBounds;
            m_PoolMono = new PoolMono<Bullet>(bulletPrefab, container, worldTransform, autoExpand);
        }
        
        public void OnStartTimer()
        {
            m_PoolMono.CreatePool(m_InitialCount);
        }

        public void CreateBullet(BulletData bulletData)
        {
            Debug.Log("[BulletSpawner] CreateBullet");
            if (!m_PoolMono.TryGet(out var bullet)) return;
                
            bullet.Construct(m_LevelBounds, bulletData);
            bullet.OnInactive += RemoveBullet;
        }

        private void RemoveBullet(Bullet bullet)
        {
            bullet.OnInactive -= this.RemoveBullet;
            m_PoolMono.InactiveObject(bullet);
        }

        public void OnFinishGame()
        {
            m_PoolMono.ClearPool();
        }

        public void OnFixedUpdate(float deltaTime)
        {
            m_PoolMono.Actives.ForEach(it => it.OnFixedUpdate(deltaTime));
        }
    }
}