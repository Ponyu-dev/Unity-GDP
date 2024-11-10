using Atomic.Contexts;
using Atomic.Elements;
using Atomic.Entities;
using Game.Scripts.Contexts.PlayerContext.InputSystem;
using Game.Scripts.Helpers;
using Unity.Mathematics;

namespace Game.Scripts.Contexts.PlayerContext.MovementSystem
{
    public sealed class PlayerMovementSystem : IContextInit, IContextUpdate
    {
        private IValue<IEntity> _character;
        private InputMap _inputMap;

        public void Init(IContext context)
        {
            _character = context.GetCharacter();
            _inputMap = context.GetInputMap();
        }

        public void Update(IContext context, float deltaTime)
        {
            if (_character.Value.GetIsDead().Value)
                return;
            var direction = _inputMap.GetMoveDirection();
            _character.Value.GetMoveDirection().Value = direction;
            _character.Value.GetAnimBoolEvent().Invoke(AnimationProperties.IS_MOVING, math.length(direction) > 0);
        }
    }
}