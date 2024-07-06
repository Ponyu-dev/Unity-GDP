using UnityEngine;
using Utils;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour
    {
        [SerializeField] private Bullet prefab;
        [SerializeField] private int initialCount = 50;
        [SerializeField] private Transform container;
        [SerializeField] private Transform worldTransform;
        
        [SerializeField] private LevelBounds levelBounds;

        private PoolMono<Bullet> m_PoolMono;
        
        private void Awake()
        {
            m_PoolMono = new PoolMono<Bullet>(prefab, initialCount, container, worldTransform);
        }

        public void CreateBullet(BulletData bulletData)
        {
            var bullet = m_PoolMono.Get();
            bullet.Construct(bulletData, levelBounds);
            bullet.OnInactive += this.RemoveBullet;
        }

        private void RemoveBullet(Bullet bullet)
        {
            bullet.OnInactive -= this.RemoveBullet;
            m_PoolMono.InactiveObject(bullet);
        }
    }
}