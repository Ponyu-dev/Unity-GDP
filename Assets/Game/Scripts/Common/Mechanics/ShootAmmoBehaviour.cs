using Atomic.Elements;
using Atomic.Entities;
using Game.Scripts.Helpers;
using UnityEngine;

namespace Game.Scripts.Common.Mechanics
{
    public sealed class ShootAmmoBehaviour : IEntityInit, IEntityUpdate, IEntityEnable, IEntityDisable
    {
        private Cycle _reloadPeriod;
        private Const<int> _maxAmmo;
        private IVariable<int> _currentAmmo;
        private Transform _firePoint;
        private BaseEvent<Transform> _shootAction;

        public void Init(IEntity entity)
        {
            _reloadPeriod = entity.GetAttackPeroid();
            _firePoint = entity.GetFirePoint();
            _currentAmmo = entity.GetCurrentAmmo();
            _maxAmmo = entity.GetMaxAmmo();
            _shootAction = entity.GetShootAction();
            _reloadPeriod.Start();
        }

        public void OnUpdate(IEntity entity, float deltaTime)
        {
            _reloadPeriod.Tick(deltaTime);
        }

        public void Enable(IEntity entity)
        {
            entity.GetAnimatorDispatcher().SubscribeOnEvent(ActionType.SHOOT, OnShoot);
            _reloadPeriod.OnCycle += Reload;
        }

        private void Reload()
        {
            if (_currentAmmo.Value >= _maxAmmo.Value) return;
            
            _currentAmmo.Value++;
        }

        private void OnShoot()
        {
            if (_currentAmmo.Value <= 0) return;
            
            _shootAction?.Invoke(_firePoint);
            
            _currentAmmo.Value--;
        }

        public void Disable(IEntity entity)
        {
            entity.GetAnimatorDispatcher().UnsubscribeOnEvent(ActionType.SHOOT, OnShoot);
            _reloadPeriod.OnCycle -= Reload;
        }
    }
}