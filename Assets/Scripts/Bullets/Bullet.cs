using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        [SerializeField] private BulletBoundObserver bulletBoundObserver;
        [SerializeField] private BulletCollisionObserver bulletCollisionObserver;
        
        public event Action<Bullet> OnInactive;

        public void Construct(BulletData bulletData, LevelBounds levelBounds)
        {
            bulletCollisionObserver.Construct(bulletData);
            bulletCollisionObserver.OnCollisionEntered += Inactive;
            
            bulletBoundObserver.Construct(levelBounds);
            bulletBoundObserver.OnExceedBounded += Inactive;
        }

        private void Inactive()
        {
            bulletCollisionObserver.OnCollisionEntered -= Inactive;
            bulletBoundObserver.OnExceedBounded -= Inactive;
            OnInactive?.Invoke(this);
        }
    }
}