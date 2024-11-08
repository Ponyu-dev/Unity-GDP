using Atomic.Elements;
using Atomic.Entities;
using Game.Scripts.Helpers;

namespace Game.Scripts.Common.Mechanics
{
    public sealed class MeleeAttackBehaviour : IEntityInit, IEntityEnable, IEntityDisable, IEntityUpdate
    {
        private Cycle _attackPeriod;
        private IValue<bool> _isRangeAttack;
        private BaseEvent<string, bool> _animBoolEvent;
        private BaseEvent<string> _animTriggerEvent;
        
        public void Init(IEntity entity)
        {
            _attackPeriod = entity.GetAttackPeroid();
            _isRangeAttack = entity.GetIsRangeAttack();
            _animBoolEvent = entity.GetAnimBoolEvent();
            _animTriggerEvent = entity.GetAnimTriggerEvent();
        }
        
        public void Enable(IEntity entity)
        {
            entity.GetIsRangeAttack().Subscribe(OnRangeAttack);
            _attackPeriod.Start();
            _attackPeriod.OnCycle += Attack;
        }

        private void OnRangeAttack(bool attack)
        {
            _animBoolEvent.Invoke(AnimationProperties.IS_MOVING, !attack);
        }
        
        private void Attack()
        {
            if (_isRangeAttack.Value)
                _animTriggerEvent.Invoke(AnimationProperties.ATTACK_MELEE);
        }

        public void Disable(IEntity entity)
        {
            entity.GetIsRangeAttack().Unsubscribe(OnRangeAttack);
            _attackPeriod.OnCycle -= Attack;
        }

        public void OnUpdate(IEntity entity, float deltaTime)
        {
            if (_isRangeAttack.Value)
                _attackPeriod.Tick(deltaTime);
        }
    }
}