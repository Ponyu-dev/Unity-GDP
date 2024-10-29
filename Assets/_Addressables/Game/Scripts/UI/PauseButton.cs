using System;
using UnityEngine;
using UnityEngine.UI;

namespace SampleGame
{
    public sealed class PauseButton : MonoBehaviour
    {
        public event Action OnClicked;
        
        [SerializeField] private Button button;
        
        private void OnEnable()
        {
            button.onClick.AddListener(InvokeOnClicked);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(InvokeOnClicked);
        }

        private void InvokeOnClicked()
        {
            Debug.Log("[PauseButton] InvokeOnClicked()");
            OnClicked?.Invoke();
        }
    }
}