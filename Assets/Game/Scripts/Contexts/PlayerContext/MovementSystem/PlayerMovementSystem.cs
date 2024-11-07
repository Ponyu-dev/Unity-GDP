using Atomic.Contexts;
using Atomic.Elements;
using Atomic.Entities;
using Game.Scripts.Contexts.PlayerContext.InputSystem;

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
            _character.Value.GetMoveDirection().Value = _inputMap.GetMoveDirection();
        }
    }
}