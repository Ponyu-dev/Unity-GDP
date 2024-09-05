using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletCollisionObserver
    {
        public event Action OnCollisionEntered;
        
        private BulletData m_BulletData;

        public void SetData(BulletData bulletData)
        {
            m_BulletData = bulletData;
        }
        
        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out DamageComponent damageComponent))
                damageComponent.OnDamage(m_BulletData);
            
            OnCollisionEntered?.Invoke();
        }
    }
}