using Atomic.Contexts;
using Atomic.Elements;
using Atomic.Entities;
using Unity.Mathematics;

namespace Game.Scripts.Contexts.ZombieContext.MovementSystem
{
    public sealed class ZombieMovementSystem : IContextInit, IContextUpdate
    {
        private IValue<IEntity> _zombie;
        private IValue<float3> _position;
        private IValue<float3> _playerPosition;

        public void Init(IContext context)
        {
            _zombie = context.GetCharacter();
            _position = _zombie.Value.GetPosition();
            _playerPosition = context.GetAttackPlayer().Value.GetPosition();
        }

        public void Update(IContext context, float deltaTime)
        {
            _zombie.Value.GetMoveDirection().Value = math.normalize(_playerPosition.Value - _position.Value);
            _zombie.Value.GetLook().Value = _playerPosition.Value;
        }
    }
}