using UnityEngine;

namespace Game.Scripts.Helpers
{
    public sealed class TriggerEventReceiver : MonoBehaviour
    {
        public event System.Action<Collider> OnEntered;
        public event System.Action<Collider> OnExited;
        
        private void OnTriggerEnter(Collider collider)
        {
            OnEntered?.Invoke(collider);
        }

        private void OnTriggerExit(Collider collider)
        {
            OnExited?.Invoke(collider);
        }
    }
}