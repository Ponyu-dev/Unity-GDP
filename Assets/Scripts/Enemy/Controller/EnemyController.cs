using System;
using UnityEngine;

namespace ShootEmUp
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyMoveAgent enemyMoveAgent;
        [SerializeField] private EnemyAttackAgent enemyAttackAgent;
        [SerializeField] private HitPointsComponent hitPointsComponent;
        public event Action<EnemyController> OnDestroyed;

        private BulletSystem m_BulletSystem;
        
        public void Construct(
            Transform worldTransform,
            Vector3 spawnPosition,
            Vector3 attackPosition,
            Transform targetTransform,
            HitPointsComponent targetHitPointsComponent,
            BulletSystem bulletSystem)
        {
            m_BulletSystem = bulletSystem;
            
            transform.SetParent(worldTransform);
            transform.position = spawnPosition;

            enemyMoveAgent.SetDestination(attackPosition);
            enemyAttackAgent.SetTargetPosition(targetTransform);
            
            enemyAttackAgent.AppendCondition(enemyMoveAgent.IsReached);
            enemyAttackAgent.AppendCondition(targetHitPointsComponent.IsHitPointsExists);

            hitPointsComponent.OnDeath += OnDeath;
            enemyAttackAgent.OnFireAction += OnFireAction;
        }

        private void OnDeath()
        {
            hitPointsComponent.OnDeath -= OnDeath;
            enemyAttackAgent.OnFireAction -= OnFireAction;
            
            OnDestroyed?.Invoke(this);
        }

        private void OnFireAction(BulletData bulletData)
        {
            if (m_BulletSystem == null) return;
            
            m_BulletSystem.CreateBullet(bulletData);
        }
    }
}