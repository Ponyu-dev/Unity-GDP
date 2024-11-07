using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace Game.Scripts.Common.Mechanics
{
    public sealed class TakeDamageBehaviour : IEntityInit, IEntityDispose
    {
        private IVariable<int> _hitPoints;
        private IVariable<bool> _isDead;
        private IValue<bool> _canTakeDamage;
        
        public void Init(IEntity entity)
        {
            _hitPoints = entity.GetHitPoints();
            _isDead = entity.GetIsDead();
            _canTakeDamage = entity.GetCanTakeDamage();

            entity.GetTakeDamageAction().Subscribe(TakeDamage);
        }
        
        private void TakeDamage(int damage)
        {
            if (!_canTakeDamage.Value)
                return;

            _hitPoints.Value -= damage;
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