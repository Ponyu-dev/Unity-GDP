using _EventBus.Scripts.Players.Hero;

namespace _EventBus.Scripts.Game.Events
{
    // Событие атаки героя
    public struct AttackedEvent
    {
        public IHeroEntity Attacker;
        public IHeroEntity Target;

        public AttackedEvent(IHeroEntity attacker, IHeroEntity target)
        {
            Attacker = attacker;
            Target = target;
        }
    }
}