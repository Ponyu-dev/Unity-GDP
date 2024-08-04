using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletCollisionObserver : MonoBehaviour
    {
        [SerializeField] private new Rigidbody2D rigidbody2D;
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        public event Action OnCollisionEntered;
        
        private BulletData m_BulletData;

        public void Construct(BulletData bulletData)
        {
            m_BulletData = bulletData;
            this.rigidbody2D.velocity = m_BulletData.velocity;
            this.gameObject.layer = m_BulletData.physicsLayer;
            this.transform.position = m_BulletData.position;
            this.spriteRenderer.color = m_BulletData.color;
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out DamageComponent damageComponent))
                damageComponent.OnDamage(m_BulletData);
            
            OnCollisionEntered?.Invoke();
        }
    }
}