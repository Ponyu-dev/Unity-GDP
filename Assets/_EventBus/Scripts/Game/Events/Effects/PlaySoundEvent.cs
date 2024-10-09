using JetBrains.Annotations;
using UnityEngine;

namespace _EventBus.Scripts.Game.Events.Effects
{
    public class PlaySoundEvent
    {
        [CanBeNull] public AudioClip Clip { get; private set; }
        
        public PlaySoundEvent([CanBeNull] AudioClip clip)
        {
            Clip = clip;
        }
    }
}