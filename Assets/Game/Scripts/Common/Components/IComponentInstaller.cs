using Atomic.Entities;

namespace Game.Scripts.Common.Components
{
    public interface IComponentInstaller
    {
        void Install(IEntity entity);
    }
}