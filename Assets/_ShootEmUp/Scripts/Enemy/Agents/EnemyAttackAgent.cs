using System;
using UnityEngine;
using Utils;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent
    { 
        private readonly WeaponData m_WeaponData;
        private readonly float m_Countdown;
        private readonly BulletConfig m_BulletConfig;
        private readonly Transform m_TargetTransform;
        private readonly IBulletSpawner m_BulletSpawner;
        
        private float m_CurrentTime;

        private readonly CompositeCondition m_Condition = new();

        public EnemyAttackAgent(
            WeaponData weaponData,
            float countdown,
            IBulletSpawner bulletSpawner,
            BulletConfig bulletConfig,
            Transform targetTransform)
        {
            m_WeaponData = weaponData;
            m_Countdown = countdown;
            m_BulletSpawner = bulletSpawner;
            m_BulletConfig = bulletConfig;
            m_TargetTransform = targetTransform;
        }

        public void AppendCondition(Func<bool> condition)
        {
            m_Condition.Append(condition);
        }

        public void Reset()
        {
            this.m_CurrentTime = this.m_Countdown;
        }
        
        public void OnFixedUpdate(float deltaTime)
        {
            if (m_Condition.IsAllFalse())
                return;

            this.m_CurrentTime -= deltaTime;
            if (!(this.m_CurrentTime <= 0)) return;
            
            this.Fire();
            this.m_CurrentTime += this.m_Countdown;
        }

        private void Fire()
        {
            var startPosition = this.m_WeaponData.position;
            var vector = (Vector2) this.m_TargetTransform.position - startPosition;
            var direction = vector.normalized;

            m_BulletSpawner.CreateBullet(
                new BulletData(
                    isPlayer: false, 
                    physicsLayer: (int) m_BulletConfig.physicsLayer, 
                    color: m_BulletConfig.color, 
                    damage: m_BulletConfig.damage, 
                    position: startPosition, 
                    velocity: direction * 2.0f)
                );
        }
    }
}