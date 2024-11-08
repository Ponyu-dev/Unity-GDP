using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace Game.Scripts.Common.Mechanics
{
    public sealed class TriggerEventBehaviour : IEntityInit, IEntityEnable, IEntityDisable
    {
        private Const<int> _damage;
        
        public void Init(IEntity entity)
        {
            _damage = entity.GetDamage();
        }

        public void Enable(IEntity entity)
        {
            entity.GetTriggerEventReceiver().OnEntered += OnTriggerEntered;
        }

        private void OnTriggerEntered(Collider obj)
        {
            if (!obj.TryGetComponent<IEntity>(out var entity))
                return;
            entity.GetTakeDamageAction().Invoke(_damage.Value);
        }

        public void Disable(IEntity entity)
        {
            entity.GetTriggerEventReceiver().OnEntered -= OnTriggerEntered;
        }
    }
}