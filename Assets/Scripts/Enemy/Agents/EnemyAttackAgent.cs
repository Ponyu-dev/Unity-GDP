using System;
using UnityEngine;
using Utils;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : MonoBehaviour
    {
        [SerializeField] private WeaponComponent weaponComponent;
        [SerializeField] private float countdown;
        [SerializeField] private BulletConfig bulletConfig;

        private Transform m_TargetTransform;
        private float m_CurrentTime;
        private BulletSystem m_BulletSystem;

        private readonly CompositeCondition m_Condition = new();

        public void Construct(BulletSystem bulletSystem,Transform targetTransform)
        {
            m_BulletSystem = bulletSystem;
            m_TargetTransform = targetTransform;
        }

        public void AppendCondition(Func<bool> condition)
        {
            m_Condition.Append(condition);
        }

        public void Reset()
        {
            this.m_CurrentTime = this.countdown;
        }

        private void FixedUpdate()
        {
            if (m_Condition.IsAllFalse())
                return;

            this.m_CurrentTime -= Time.fixedDeltaTime;
            if (this.m_CurrentTime <= 0)
            {
                this.Fire();
                this.m_CurrentTime += this.countdown;
            }
        }

        private void Fire()
        {
            var startPosition = this.weaponComponent.position;
            var vector = (Vector2) this.m_TargetTransform.position - startPosition;
            var direction = vector.normalized;

            m_BulletSystem.CreateBullet(
                new BulletData(
                    isPlayer: false, 
                    physicsLayer: (int) bulletConfig.physicsLayer, 
                    color: bulletConfig.color, 
                    damage: bulletConfig.damage, 
                    position: startPosition, 
                    velocity: direction * 2.0f)
                );
        }
    }
}