using Atomic.Entities;

namespace Game.Scripts.Contexts.GameContext.EntityPool
{
    public interface IEntityPool
    {
        public IEntity Rent();
        public void Return(IEntity entity);
    }
}