using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface IBullet
    {
        event Action<Bullet, Collision2D> OnCollisionEntered;
        void SetArgs(Args args);
        Args GetArgs();
    }
    
    public sealed class Bullet : MonoBehaviour, IBullet
    {
        public event Action<Bullet, Collision2D> OnCollisionEntered;
        private Args _args;

        [SerializeField]
        private new Rigidbody2D rigidbody2D;

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            this.OnCollisionEntered?.Invoke(this, collision);
        }

        public void SetArgs(Args args)
        {
            _args = args;
            this.rigidbody2D.velocity = _args._velocity;
            this.gameObject.layer = _args._physicsLayer;
            this.transform.position = _args._position;
            this.spriteRenderer.color = _args._color;
        }

        public Args GetArgs() => _args;
    }
}