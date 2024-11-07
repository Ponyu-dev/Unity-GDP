using System.Collections.Generic;
using Atomic.Entities;

namespace Game.Scripts.Contexts.Base.EntityPool
{
    public interface IEntityPool
    {
        public IEntity Rent();
        public void Return(IEntity entity);
        public IReadOnlyList<IEntity> GetActives();
        public int CountActives();
    }
}