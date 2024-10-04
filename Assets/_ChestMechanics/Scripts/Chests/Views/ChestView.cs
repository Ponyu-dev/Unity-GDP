using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _ChestMechanics.Chests.System
{
    public interface IChestView
    {
        public void SetTimer(string timer);
        public void StartAnimation(string animationName);
        public event Action OnChestOpen;
    }
    
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Button))]
    public class ChestView : MonoBehaviour, IChestView
    {
        private Action _onChestOpen;

        // Свойство с подпиской и отпиской
        public event Action OnChestOpen
        {
            add { _onChestOpen += value; }
            remove { _onChestOpen -= value; }
        }
        
        [SerializeField] private TextMeshProUGUI txtTimer;
        [SerializeField] private Button btnChest;
        [SerializeField] private Animator animator;

        private void OnEnable()
        {
            btnChest.onClick.AddListener(OnChestOpened);
        }

        private void OnDisable()
        {
            btnChest.onClick.RemoveListener(OnChestOpened);
        }

        private void OnChestOpened()
        {
            _onChestOpen?.Invoke();
        }

        public void SetTimer(string timer)
        {
            txtTimer.text = timer;
        }

        public void StartAnimation(string animationName)
        {
            animator.Play(animationName);
        }
    }
}