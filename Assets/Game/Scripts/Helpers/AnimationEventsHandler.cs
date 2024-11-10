using System;
using UnityEngine;

namespace Game.Scripts.Helpers
{
    public interface IShootAnimationReceiver
    {
        event Action OnShoot;
    }
    
    public sealed class AnimationEventsHandler : MonoBehaviour, IShootAnimationReceiver
    {
        public event Action OnShoot;
        
        public void Shoot()
        {
            Debug.Log("[AnimationEventsHandler] Shoot");
            OnShoot?.Invoke();
        }
    }
}