using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : MonoBehaviour
    {
        public Action<Args> OnFireAction;

        [SerializeField] private WeaponComponent weaponComponent;
        [SerializeField] private EnemyMoveAgent moveAgent;
        [SerializeField] private float countdown;
        [SerializeField] private BulletConfig _bulletConfig;

        private GameObject target;
        private float currentTime;

        public void SetTarget(GameObject target)
        {
            this.target = target;
        }

        public void Reset()
        {
            this.currentTime = this.countdown;
        }

        private void FixedUpdate()
        {
            if (!this.moveAgent.IsReached)
            {
                return;
            }
            
            if (!this.target.GetComponent<HitPointsComponent>().IsHitPointsExists())
            {
                return;
            }

            this.currentTime -= Time.fixedDeltaTime;
            if (this.currentTime <= 0)
            {
                this.Fire();
                this.currentTime += this.countdown;
            }
        }

        private void Fire()
        {
            var startPosition = this.weaponComponent.Position;
            var vector = (Vector2) this.target.transform.position - startPosition;
            var direction = vector.normalized;

            this.OnFireAction.Invoke(
                new Args(
                    isPlayer: false, 
                    physicsLayer: (int) _bulletConfig.physicsLayer, 
                    color: _bulletConfig.color, 
                    damage: _bulletConfig.damage, 
                    position: startPosition, 
                    velocity: direction * 2.0f)
                );
        }
    }
}