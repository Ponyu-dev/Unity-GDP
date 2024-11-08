using Atomic.Elements;
using Atomic.Entities;
using Game.Scripts.Helpers;
using UnityEngine;

namespace Game.Scripts.Common.Mechanics
{
    public sealed class TakeDamageBehaviour : IEntityInit, IEntityDispose
    {
        private IVariable<int> _hitPoints;
        private IVariable<bool> _isDead;
        private IValue<bool> _canTakeDamage;
        private BaseEvent<string> _animTriggerEvent;
        
        public void Init(IEntity entity)
        {
            _hitPoints = entity.GetHitPoints();
            _isDead = entity.GetIsDead();
            _canTakeDamage = entity.GetCanTakeDamage();
            _animTriggerEvent = entity.GetAnimTriggerEvent();

            entity.GetTakeDamageAction().Subscribe(TakeDamage);
        }
        
        private void TakeDamage(int damage)
        {
            if (!_canTakeDamage.Value)
                return;

            _hitPoints.Value -= damage;
            _animTriggerEvent?.Invoke(AnimationProperties.TAKE_DAMAGE);
            Debug.Log($"Take damage = {damage}");

            if (_hitPoints.Value <= 0)
                _isDead.Value = true;
        }

        public void Dispose(IEntity entity)
        {
            entity.GetTakeDamageAction().Unsubscribe(TakeDamage);
        }
    }
}