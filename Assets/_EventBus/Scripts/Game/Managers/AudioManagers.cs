using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace _EventBus.Scripts.Game.Managers
{
    public class AudioManagers
    {
        public UniTask PlaySoundAsync([CanBeNull] AudioClip sound)
        {
            if (sound == null) UniTask.Delay(100);
            
            AudioPlayer.Instance.PlaySound(sound);
        
            // Здесь можно добавить задержку, чтобы подождать, пока звук не закончится
            return UniTask.Delay((int)(sound.length * 1000)); // Время ожидания в миллисекундах
        }
    }
}