using System;
using UnityEngine;

namespace ShootEmUp
{
    public class Enemy : MonoBehaviour, IFixedUpdateGameListener
    {
        [SerializeField] private EnemyMoveAgent enemyMoveAgent;
        [SerializeField] private EnemyAttackAgent enemyAttackAgent;
        [SerializeField] private HitPointsComponent m_HitPointsComponent;
        
        //Решил не выносить это в другой класс.
        //Так как это event который уведомляет EnemySystem. О том что Enemy умер.
        public event Action<Enemy> OnDeathbed;
        
        public void Construct(
            Vector3 spawnPosition,
            Vector3 attackPosition,
            Transform targetTransform,
            IHitPointsComponent targetHitPointsComponent,
            IBulletSpawner bulletSpawner)
        {
            transform.position = spawnPosition;

            enemyMoveAgent.SetDestination(attackPosition);
            
            enemyAttackAgent.Construct(bulletSpawner, targetTransform);
            enemyAttackAgent.AppendCondition(enemyMoveAgent.IsReached);
            enemyAttackAgent.AppendCondition(targetHitPointsComponent.IsHitPointsExists);

            m_HitPointsComponent.OnDeath += OnDeath;
        }

        private void OnDeath()
        {
            m_HitPointsComponent.OnDeath -= OnDeath;
            OnDeathbed?.Invoke(this);
        }

        public void OnFixedUpdate(float deltaTime)
        {
            enemyAttackAgent.OnFixedUpdate(deltaTime);
            enemyMoveAgent.OnFixedUpdate(deltaTime);
        }
    }
}