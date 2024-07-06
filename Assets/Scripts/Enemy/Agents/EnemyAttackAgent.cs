using System;
using UnityEngine;
using Utils;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : MonoBehaviour
    {
        public Action<BulletData> OnFireAction;

        [SerializeField] private WeaponComponent _weaponComponent;
        [SerializeField] private float _countdown;
        [SerializeField] private BulletConfig _bulletConfig;

        private Vector3 _targetPosition;
        private float _currentTime;

        private readonly CompositeConsition _condition = new();

        public void SetTargetPosition(Vector3 targetPosition)
        {
            this._targetPosition = targetPosition;
        }

        public void AppendCondition(Func<bool> condition)
        {
            _condition.Append(condition);
        }

        public void Reset()
        {
            this._currentTime = this._countdown;
        }

        private void FixedUpdate()
        {
            if (_condition.IsAllFalse())
                return;

            this._currentTime -= Time.fixedDeltaTime;
            if (this._currentTime <= 0)
            {
                this.Fire();
                this._currentTime += this._countdown;
            }
        }

        private void Fire()
        {
            var startPosition = this._weaponComponent.Position;
            var vector = (Vector2) this._targetPosition - startPosition;
            var direction = vector.normalized;

            this.OnFireAction.Invoke(
                new BulletData(
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