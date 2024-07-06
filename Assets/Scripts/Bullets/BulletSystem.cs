using UnityEngine;
using Utils;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour
    {
        [SerializeField] private Bullet prefab;
        [SerializeField] private int initialCount = 50;
        [SerializeField] private bool autoExpand = true;
        [SerializeField] private Transform container;
        [SerializeField] private Transform worldTransform;
        
        [SerializeField] private LevelBounds levelBounds;

        private PoolMono<Bullet> m_PoolMono;
        
        private void Awake()
        {
            m_PoolMono = new PoolMono<Bullet>(prefab, initialCount, container, worldTransform, autoExpand);
        }

        public void CreateBullet(BulletData bulletData)
        {
            if (!m_PoolMono.TryGet(out var bullet)) return;
                
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