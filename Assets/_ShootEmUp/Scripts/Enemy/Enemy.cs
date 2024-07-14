using System;
using UnityEngine;

namespace ShootEmUp
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyMoveAgent enemyMoveAgent;
        [SerializeField] private EnemyAttackAgent enemyAttackAgent;
        [SerializeField] private HitPointsComponent hitPointsComponent;
        
        //Решил не выносить это в другой класс.
        //Так как это event который уведомляет EnemySystem. О том что Enemy умер.
        public event Action<Enemy> OnDeathbed;
        
        public void Construct(
            Vector3 spawnPosition,
            Vector3 attackPosition,
            Transform targetTransform,
            HitPointsComponent targetHitPointsComponent,
            BulletSystem bulletSystem)
        {
            transform.position = spawnPosition;

            enemyMoveAgent.SetDestination(attackPosition);
            
            enemyAttackAgent.Construct(bulletSystem, targetTransform);
            enemyAttackAgent.AppendCondition(enemyMoveAgent.IsReached);
            enemyAttackAgent.AppendCondition(targetHitPointsComponent.IsHitPointsExists);

            hitPointsComponent.OnDeath += OnDeath;
        }

        private void OnDeath()
        {
            hitPointsComponent.OnDeath -= OnDeath;
            OnDeathbed?.Invoke(this);
        }
    }
}