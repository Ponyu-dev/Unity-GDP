using Atomic.Entities;

namespace Game.Scripts.Entities
{
    public sealed class ZombieEntityInstaller : EntityInstaller
    {
        public override void Install(IEntity entity)
        {
            base.Install(entity);
            entity.AddZombieTag();
        }
    }
}