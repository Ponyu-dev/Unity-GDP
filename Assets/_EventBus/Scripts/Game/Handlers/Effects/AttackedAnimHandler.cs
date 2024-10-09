using System;
using _EventBus.Scripts.Game.Events;
using _EventBus.Scripts.Game.Events.Effects;
using _EventBus.Scripts.Game.Presenters;
using JetBrains.Annotations;
using VContainer.Unity;

namespace _EventBus.Scripts.Game.Handlers.Effects
{
    [UsedImplicitly]
    public class AttackedAnimHandler : IInitializable, IDisposable
    {
        private readonly EventBus _eventBus;
        
        public AttackedAnimHandler(EventBus eventBus)
        {
            _eventBus = eventBus;
        }
        
        public void Initialize()
        {
            _eventBus.Subscribe<AttackedAnimEvent>(OnAttackedAnim);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<AttackedAnimEvent>(OnAttackedAnim);
        }

        private async void OnAttackedAnim(AttackedAnimEvent evt)
        {
            if (!evt.Attacker.TryGetComponent<IHeroPresenter>(out var attackerPresenter) ||
                !evt.Target.TryGetComponent<IHeroPresenter>(out var targetPresenter))
                return;
            
            await attackerPresenter.AnimateAttack(targetPresenter.GetHeroView());
            await _eventBus.RaiseEvent(new AttackedEvent(evt.Attacker, evt.Target));
        }
    }
}