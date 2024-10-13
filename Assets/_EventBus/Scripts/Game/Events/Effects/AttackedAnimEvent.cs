using _EventBus.Scripts.Players.Hero;

namespace _EventBus.Scripts.Game.Events.Effects
{
    public struct AttackedAnimEvent
    {
        public readonly IHeroEntity Attacker;
        public readonly IHeroEntity Target;

        public AttackedAnimEvent(IHeroEntity attacker, IHeroEntity target)
        {
            Attacker = attacker;
            Target = target;
        }
    }
}