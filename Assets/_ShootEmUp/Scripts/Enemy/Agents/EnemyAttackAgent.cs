using System;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : MonoBehaviour, IGameFixedUpdateListener
    {
        [FormerlySerializedAs("weaponComponent")] [SerializeField] private WeaponData weaponData;
        [SerializeField] private float countdown;
        [SerializeField] private BulletConfig bulletConfig;

        private Transform m_TargetTransform;
        private float m_CurrentTime;
        private BulletSpawner m_BulletSpawner;

        private readonly CompositeCondition m_Condition = new();

        public void Construct(BulletSpawner bulletSpawner,Transform targetTransform)
        {
            m_BulletSpawner = bulletSpawner;
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
        
        public void OnFixedUpdate(float deltaTime)
        {
            if (m_Condition.IsAllFalse())
                return;

            this.m_CurrentTime -= deltaTime;
            if (this.m_CurrentTime <= 0)
            {
                this.Fire();
                this.m_CurrentTime += this.countdown;
            }
        }

        private void Fire()
        {
            var startPosition = this.weaponData.position;
            var vector = (Vector2) this.m_TargetTransform.position - startPosition;
            var direction = vector.normalized;

            m_BulletSpawner.CreateBullet(
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