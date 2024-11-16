using Atomic.Elements;
using Atomic.Entities;
using Game.Scripts.Helpers;
using UnityEngine;

namespace Game.Scripts.Common.Mechanics
{
    public sealed class DeadBehaviour : IEntityInit, IEntityDispose
    {
        private IEntity _entity;
        private BaseEvent<string, bool> _animBoolEvent;
        private BaseEvent<IEntity> _deadAction;
        
        public void Init(IEntity entity)
        {
            _entity = entity;
            _entity.GetIsDead().Subscribe(OnDead);
            _animBoolEvent = _entity.GetAnimBoolEvent();
            _deadAction = _entity.GetDeadAction();
        }

        private void OnDead(bool isDead)
        {
            _animBoolEvent?.Invoke(AnimationProperties.IS_DEAD, isDead);

            if (isDead)
            {
                Debug.Log("OnDead");
                _deadAction.Invoke(_entity);
            }
        }

        public void Dispose(IEntity entity)
        {
            entity.GetIsDead().Unsubscribe(OnDead);
        }
    }
}