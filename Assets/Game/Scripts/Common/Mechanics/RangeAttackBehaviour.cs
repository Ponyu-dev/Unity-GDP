using Atomic.Elements;
using Atomic.Entities;
using Unity.Mathematics;

namespace Game.Scripts.Common.Mechanics
{
    public sealed class RangeAttackBehaviour : IEntityInit, IEntityUpdate
    {
        private Const<float> _attackRange;
        private IValue<float3> _position;
        private IValue<float3> _positionPlayer;
        private IVariable<bool> _isAttackRange;
        
        public void Init(IEntity entity)
        {
            _attackRange = entity.GetAttackRange();
            _position = entity.GetPosition();
            _positionPlayer = entity.GetAttackEntity().Value.GetPosition();
            _isAttackRange = entity.GetIsRangeAttack();
        }

        public void OnUpdate(IEntity entity, float deltaTime)
        {
            var distance = math.distance(_positionPlayer.Value, _position.Value);
            _isAttackRange.Value = distance < _attackRange.Value;
        }
    }
}