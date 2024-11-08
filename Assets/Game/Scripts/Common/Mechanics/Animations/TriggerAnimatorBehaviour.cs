using Atomic.Entities;
using UnityEngine;

namespace Game.Scripts.Common.Mechanics.Animations
{
    public sealed class TriggerAnimatorBehaviour : IEntityInit, IEntityDispose
    {
        private Animator _animator;
        
        public void Init(IEntity entity)
        {
            _animator = entity.GetAnimator();
            entity.GetAnimTriggerEvent().Subscribe(OnTriggered);
        }
        
        private void OnTriggered(string trigger)
        {
            _animator.SetTrigger(trigger);
        }

        public void Dispose(IEntity entity)
        {
            entity.GetAnimTriggerEvent().Unsubscribe(OnTriggered);
        }
    }
}