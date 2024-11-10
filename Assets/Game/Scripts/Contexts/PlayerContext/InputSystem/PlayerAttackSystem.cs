using Atomic.Contexts;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace Game.Scripts.Contexts.PlayerContext.InputSystem
{
    public sealed class PlayerAttackSystem : IContextInit, IContextUpdate
    {
        private BaseEvent _attackAction;
        
        public void Init(IContext context)
        {
            _attackAction = context.GetCharacter().Value.GetAttackAction();
        }

        public void Update(IContext context, float deltaTime)
        {
            if (Input.GetMouseButtonDown(0))
                _attackAction?.Invoke();
        }
    }
}