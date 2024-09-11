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

        private readonly MoveComponent m_MoveComponent = new();
        
        private EnemyMoveAgent m_EnemyMoveAgent;
        private EnemyAttackAgent m_EnemyAttackAgent;
        private IHitPointsComponent m_HitPointsComponent;
        
        //Решил не выносить это в другой класс.
        //Так как это event который уведомляет EnemySystem. О том что Enemy умер.
        //OnDeathbed - это типо не умер. А присмерти :)
        public event Action<Enemy> OnDeath;

        public void Construct(
            Vector3 spawnPosition,
            Vector3 attackPosition,
            float countdown,
            Transform targetTransform,
            IBulletSpawner bulletSpawner,
            BulletConfig bulletConfig)
        {
            transform.position = spawnPosition;

            m_HitPointsComponent = new HitPointsComponent(hitPointsData);
            m_HitPointsComponent.OnDeath += Death;
            
            m_MoveComponent.Construct(moveData);
            m_EnemyMoveAgent = new EnemyMoveAgent(m_MoveComponent, transform, attackPosition);
            
            m_EnemyAttackAgent = new EnemyAttackAgent(weaponData, countdown, bulletSpawner, bulletConfig, targetTransform);
            m_EnemyAttackAgent.AppendCondition(m_EnemyMoveAgent.IsReached);

            var damageComponent = GetComponent<DamageComponent>();
            damageComponent.Construct(m_HitPointsComponent, teamData);
        }

        private void Death()
        {
            m_HitPointsComponent.OnDeath -= Death;
            OnDeath?.Invoke(this);
        }

        public void OnAttack(float deltaTime)
        {
            m_EnemyAttackAgent.OnFixedUpdate(deltaTime);
        }

        public void OnMove(float deltaTime)
        {
            m_EnemyMoveAgent.OnFixedUpdate(deltaTime);
        }
    }
}