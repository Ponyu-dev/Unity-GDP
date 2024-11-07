using Atomic.Contexts;
using Atomic.Elements;
using Atomic.Entities;
using Game.Scripts.Contexts.Base.EntityPool;
using Unity.Mathematics;

namespace Game.Scripts.Contexts.ZombieContext.MovementSystem
{
    public sealed class ZombieMovementSystem : IContextInit, IContextUpdate
    {
        private IEntityPool _zombiePool;
        private IValue<float3> _playerPosition;

        public void Init(IContext context)
        {
            _zombiePool = context.GetZombieSystemData().pool;
            _playerPosition = context.GetAttackPlayer().Value.GetPosition();
        }

        public void Update(IContext context, float deltaTime)
        {
            foreach (var activeEntity in _zombiePool.GetActives())
            {
                var position = activeEntity.GetPosition().Value;

                activeEntity.GetMoveDirection().Value = math.normalize(_playerPosition.Value - position);
                activeEntity.GetLook().Value = _playerPosition.Value;
            }
        }
    }
}