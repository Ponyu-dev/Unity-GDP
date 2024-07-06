using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet> OnCollisionEntered;
        private BulletData m_BulletData;

        [SerializeField]
        private new Rigidbody2D rigidbody2D;

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out CollisionEnterComponent collisionEnterComponent))
                collisionEnterComponent.OnCollisionEntered(m_BulletData);
            
            this.OnCollisionEntered?.Invoke(this);
        }

        public void Construct(BulletData bulletData)
        {
            m_BulletData = bulletData;
            this.rigidbody2D.velocity = m_BulletData.velocity;
            this.gameObject.layer = m_BulletData.physicsLayer;
            this.transform.position = m_BulletData.position;
            this.spriteRenderer.color = m_BulletData.color;
        }
    }
}