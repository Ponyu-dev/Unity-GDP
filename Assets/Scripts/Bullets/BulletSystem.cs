using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour
    {
        [SerializeField] private int initialCount = 50;
        [SerializeField] private Transform container;
        [SerializeField] private Bullet prefab;
        [SerializeField] private Transform worldTransform;
        [SerializeField] private LevelBounds levelBounds;

        private readonly Queue<Bullet> m_BulletPool = new();
        private readonly HashSet<Bullet> m_ActiveBullets = new();
        private readonly List<Bullet> m_Cache = new();
        
        private void Awake()
        {
            for (var i = 0; i < this.initialCount; i++)
            {
                var bullet = Instantiate(this.prefab, this.container);
                this.m_BulletPool.Enqueue(bullet);
            }
        }
        
        private void FixedUpdate()
        {
            this.m_Cache.Clear();
            this.m_Cache.AddRange(this.m_ActiveBullets);

            for (int i = 0, count = this.m_Cache.Count; i < count; i++)
            {
                var bullet = this.m_Cache[i];
                if (!this.levelBounds.InBounds(bullet.transform.position))
                {
                    this.RemoveBullet(bullet);
                }
            }
        }

        public void FlyBulletByArgs(BulletData bulletData)
        {
            if (this.m_BulletPool.TryDequeue(out var bullet))
            {
                bullet.transform.SetParent(this.worldTransform);
            }
            else
            {
                bullet = Instantiate(this.prefab, this.worldTransform);
            }

            bullet.Construct(bulletData);
            
            if (this.m_ActiveBullets.Add(bullet))
            {
                bullet.OnCollisionEntered += this.OnBulletCollision;
            }
        }
        
        private void OnBulletCollision(Bullet bullet)
        {
            this.RemoveBullet(bullet);
        }

        private void RemoveBullet(Bullet bullet)
        {
            if (!this.m_ActiveBullets.Remove(bullet)) return;
            
            bullet.OnCollisionEntered -= this.OnBulletCollision;
            bullet.transform.SetParent(this.container);
            this.m_BulletPool.Enqueue(bullet);
        }
    }
}