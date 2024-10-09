using _EventBus.Scripts.Game.Presenters;

namespace _EventBus.Scripts.Game.Events
{
    public struct AttackedAnimEvent
    {
        public readonly IHeroPresenter Attacker;
        public readonly IHeroPresenter Target;

        public AttackedAnimEvent(IHeroPresenter attacker, IHeroPresenter target)
        {
            Attacker = attacker;
            Target = target;
        }
    }
}