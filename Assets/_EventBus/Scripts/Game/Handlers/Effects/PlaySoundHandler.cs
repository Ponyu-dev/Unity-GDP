using System;
using _EventBus.Scripts.Game.Events.Effects;
using _EventBus.Scripts.Game.Managers;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace _EventBus.Scripts.Game.Handlers.Effects
{
    public class PlaySoundHandler : IInitializable, IDisposable
    {
        private readonly EventBus _eventBus;
        private readonly AudioManagers _audioManagers;
        
        public PlaySoundHandler(
            EventBus eventBus,
            AudioManagers audioManagers)
        {
            _eventBus = eventBus;
            _audioManagers = audioManagers;
        }
        
        public void Initialize()
        {
            _eventBus.Subscribe<PlaySoundEvent>(OnPlaySounded);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<PlaySoundEvent>(OnPlaySounded);
        }

        private async UniTask OnPlaySounded(PlaySoundEvent evt)
        {
            await _audioManagers.PlaySoundAsync(evt.Clip);
        }
    }
}