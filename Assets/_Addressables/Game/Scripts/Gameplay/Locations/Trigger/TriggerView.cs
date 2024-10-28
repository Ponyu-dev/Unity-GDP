using System;
using UnityEngine;

namespace _Addressables.Game.Scripts.Gameplay.Locations.Trigger
{
    public interface ITriggerView
    {
        public event Action<string> OnTriggerEntered;
    }
    
    public sealed class TriggerView : MonoBehaviour, ITriggerView
    {
        public event Action<string> OnTriggerEntered;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"TriggerView {gameObject.name}");
            OnTriggerEntered?.Invoke(gameObject.name);
        }
    }
}