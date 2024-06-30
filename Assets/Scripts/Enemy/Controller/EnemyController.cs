using System;
using UnityEngine;

namespace ShootEmUp
{
    public class EnemyController : MonoBehaviour, IEnemyController<EnemyController>
    {
        [SerializeField] private EnemyMoveAgent _enemyMoveAgent;
        [SerializeField] private EnemyAttackAgent _enemyAttackAgent;
        [SerializeField] private HitPointsComponent _hitPointsComponent;

        private BulletSystem _bulletSystem;

        public event Action<EnemyController> OnDestroyed;

        public void Construct(
            Transform worldTransform,
            Vector3 spawnPosition,
            Vector3 attackPosition,
            GameObject target,
            BulletSystem bulletSystem)
        {
            _bulletSystem = bulletSystem;
            
            transform.SetParent(worldTransform);
            transform.position = spawnPosition;

            _enemyMoveAgent.SetDestination(attackPosition);
            _enemyAttackAgent.SetTarget(target);

            _hitPointsComponent.OnDeath += OnDeath;
            _enemyAttackAgent.OnFireAction += OnFireAction;
        }

        private void OnDeath()
        {
            _hitPointsComponent.OnDeath -= OnDeath;
            _enemyAttackAgent.OnFireAction -= OnFireAction;
            
            OnDestroyed?.Invoke(this);
        }

        private void OnFireAction(Args args)
        {
            if (_bulletSystem == null) return;
            
            _bulletSystem.FlyBulletByArgs(args);
        }
    }
}