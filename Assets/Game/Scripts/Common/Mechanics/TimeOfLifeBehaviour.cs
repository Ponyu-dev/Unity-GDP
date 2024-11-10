using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace Game.Scripts.Common.Mechanics
{
    public sealed class TimeOfLifeBehaviour : IEntityInit, IEntityEnable, IEntityDisable, IEntityUpdate
    {
        private IEntity _entity;
        private Cycle _periodLife;
        private BaseEvent<IEntity> _deadAction;
        
        public void Init(IEntity entity)
        {
            _entity = entity;
            _periodLife = entity.GetLifeTime();
            _deadAction = entity.GetDeadAction();
        }

        public void Enable(IEntity entity)
        {
            _periodLife.OnCycle += OnDead;
            entity.GetTriggerEventReceiver().OnEntered += OnTriggerEntered;
        }

        private void OnDead()
        {
            _periodLife.Stop();
            _deadAction?.Invoke(_entity);
        }
        

        private void OnTriggerEntered(Collider obj)
        {
            OnDead();
        }

        public void Disable(IEntity entity)
        {
            _periodLife.OnCycle -= OnDead;
            entity.GetTriggerEventReceiver().OnEntered -= OnTriggerEntered;
        }

        public void OnUpdate(IEntity entity, float deltaTime)
        {
            _periodLife.Tick(deltaTime);
        }
    }
}