using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class Bullet : MonoBehaviour, IFixedUpdateGameListener
    {
        private Rigidbody2D m_Rigidbody2D;
        private SpriteRenderer m_SpriteRenderer;
        
        private BulletBoundObserver m_BulletBoundObserver;
        private BulletCollisionObserver m_BulletCollisionObserver;
        
        public event Action<Bullet> OnInactive;
        private LevelBounds m_LevelBounds;

        private void Awake()
        {
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        public void Construct(LevelBounds levelBounds, BulletData bulletData)
        {
            m_LevelBounds = levelBounds;

            m_BulletCollisionObserver = new BulletCollisionObserver();
            m_BulletBoundObserver = new BulletBoundObserver(m_LevelBounds);
            
            if (m_Rigidbody2D == null) m_Rigidbody2D = GetComponent<Rigidbody2D>();
            if (m_SpriteRenderer == null) m_SpriteRenderer = GetComponent<SpriteRenderer>();
            
            this.m_Rigidbody2D.velocity = bulletData.velocity;
            this.gameObject.layer = bulletData.physicsLayer;
            this.transform.position = bulletData.position;
            this.m_SpriteRenderer.color = bulletData.color;
            
            m_BulletCollisionObserver.SetData(bulletData);
            m_BulletCollisionObserver.OnCollisionEntered += Inactive;
            m_BulletBoundObserver.OnExceedBounded += Inactive;
        }

        private void Inactive()
        {
            m_BulletCollisionObserver.OnCollisionEntered -= Inactive;
            m_BulletBoundObserver.OnExceedBounded -= Inactive;
            OnInactive?.Invoke(this);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            m_BulletCollisionObserver.OnCollisionEnter2D(collision);
        }

        public void OnFixedUpdate(float deltaTime)
        {
            Debug.Log("[Bullet] OnFixedUpdate");
            m_BulletBoundObserver.CheckInBounds(transform.position);
        }
    }
}