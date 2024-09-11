using System;
using UnityEngine;

namespace ShootEmUp
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] public MoveData moveData;
        [SerializeField] public HitPointsData hitPointsData;
        [SerializeField] public TeamData teamData;
        [SerializeField] public WeaponData weaponData;

        private MoveComponent m_MoveComponent = new MoveComponent();
        
        private EnemyMoveAgent m_EnemyMoveAgent;
        //[SerializeField] private EnemyAttackAgent enemyAttackAgent;
        
        //Решил не выносить это в другой класс.
        //Так как это event который уведомляет EnemySystem. О том что Enemy умер.
        public event Action<Enemy> OnDeathbed;
        
        public void Construct(
            Vector3 spawnPosition,
            Vector3 attackPosition)
            //Transform targetTransform,
            //IBulletSpawner bulletSpawner)
        {
            transform.position = spawnPosition;
            
            m_MoveComponent.Construct(moveData);
            m_EnemyMoveAgent = new EnemyMoveAgent(m_MoveComponent, transform, attackPosition);

            /*enemyMoveAgent.SetDestination(attackPosition);
            enemyAttackAgent.Construct(bulletSpawner, targetTransform);
            enemyAttackAgent.AppendCondition(enemyMoveAgent.IsReached);*/
            //enemyAttackAgent.AppendCondition(targetHitPointsComponent.IsHitPointsExists);
        }

        private void OnDeath()
        {
            OnDeathbed?.Invoke(this);
        }

        public void OnAttack(float deltaTime)
        {
            //enemyAttackAgent.OnFixedUpdate(deltaTime);
        }

        public void OnMove(float deltaTime)
        {
            m_EnemyMoveAgent.OnFixedUpdate(deltaTime);
        }
    }
}