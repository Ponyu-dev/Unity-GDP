using Atomic.Entities;
using UnityEngine;

namespace Game.Scripts.Common.Mechanics.Animations
{
    public sealed class BoolAnimatorBehaviour : IEntityInit, IEntityDispose
    {
        private Animator _animator;

        public void Init(IEntity entity)
        {
            _animator = entity.GetAnimator();
            entity.GetAnimBoolEvent().Subscribe(OnEventBool);
        }
        
        private void OnEventBool(string name, bool value)
        {
            if (_animator.GetBool(name) == value)
                return;

            _animator.SetBool(name, value);
        }

        public void Dispose(IEntity entity)
        {
            entity.GetAnimBoolEvent().Unsubscribe(OnEventBool);
        }
    }
}