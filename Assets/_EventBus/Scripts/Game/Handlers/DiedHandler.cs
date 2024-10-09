using System;
using _EventBus.Scripts.Game.Events;
using _EventBus.Scripts.Game.Events.Effects;
using _EventBus.Scripts.Game.Factories;
using _EventBus.Scripts.Players.Components;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using VContainer.Unity;

namespace _EventBus.Scripts.Game.Handlers
{
    //TODO Может быт переименовать в DestroyHandler ???
    [UsedImplicitly]
    public class DiedHandler : IInitializable, IDisposable
    {
        private readonly EventBus _eventBus;
        private readonly IHeroFactory _heroFactory;
        
        public DiedHandler(
            EventBus eventBus,
            IHeroFactory heroFactory)
        {
            _eventBus = eventBus;
            _heroFactory = heroFactory;
        }
        
        public void Initialize()
        {
            _eventBus.Subscribe<DiedEvent>(OnDied);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<DiedEvent>(OnDied);
        }

        private async UniTask OnDied(DiedEvent evt)
        {
            var clip = evt.Target.DeathClip();
            await _eventBus.RaiseEvent(new PlaySoundEvent(clip));
            
            if (evt.Target.TryGetComponent<DestroyComponent>(out var component))
                component.Destroy();
            _heroFactory.RemoveEntity(evt.Target.HeroType);
        }
    }
}