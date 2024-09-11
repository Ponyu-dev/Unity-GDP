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
        ITimerGameListener,
        IFixedUpdateGameListener,
        IFinishGameListener,
        IBulletSpawner
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
        
        //Когда нажали на Start. И идет отсчет. Создаем пул. 
        public void OnStartTimer()
        {
            Debug.Log("[BulletSpawner] OnStartTimer");
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
        
        //При финише игры. Очищаем пул.
        public void OnFinishGame()
        {
            Debug.Log("[BulletSpawner] OnFinishGame");
            m_PoolMono.ClearPool();
        }

        public void OnFixedUpdate(float deltaTime)
        {
            Debug.Log("[BulletSpawner] OnFixedUpdate");
            m_PoolMono.Actives.ForEach(it => it.OnFixedUpdate(deltaTime));
        }
    }
}