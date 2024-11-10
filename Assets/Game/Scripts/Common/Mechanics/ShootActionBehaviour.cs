using Atomic.Elements;
using Atomic.Entities;
using Game.Scripts.Helpers;
using UnityEngine;

namespace Game.Scripts.Common.Mechanics
{
    public sealed class ShootActionBehaviour : IEntityInit, IEntityEnable, IEntityDisable, IEntityUpdate
    {
        private IValue<bool> _canShoot;
        private Countdown _countdown;
        private BaseEvent _attackAction;
        private BaseEvent<string> _attackEvent;
        
        public void Init(IEntity entity)
        {
            _attackAction = entity.GetAttackAction();
            _countdown = entity.GetAttackCountdown();
            _canShoot = entity.GetCanAttack();
            _attackEvent = entity.GetAnimTriggerEvent();
        }

        public void Enable(IEntity entity)
        {
            _attackAction.Subscribe(OnAttackAction);
        }

        private void OnAttackAction()
        {
            if (_countdown.IsPlaying() || !_canShoot.Value)
            {
                Debug.Log("[ShootActionBehaviour] Dont Shoot");
                return;
            }
            
            Debug.Log("[ShootActionBehaviour] Shoot");
            _attackEvent?.Invoke(AnimationProperties.SHOOT);
            _countdown.ResetTime();
            _countdown.Play();
        }

        public void Disable(IEntity entity)
        {
            _attackAction.Unsubscribe(OnAttackAction);
        }

        public void OnUpdate(IEntity entity, float deltaTime)
        {
            _countdown.Tick(deltaTime);
        }
    }
}