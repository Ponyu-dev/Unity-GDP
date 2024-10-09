using UnityEngine;

namespace _EventBus.Scripts.Game.Events.Effects
{
    public class PlaySoundEvent
    {
        public AudioClip Clip { get; private set; }
        
        public PlaySoundEvent(AudioClip clip)
        {
            Clip = clip;
        }
    }
}