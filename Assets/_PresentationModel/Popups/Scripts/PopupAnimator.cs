using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Popups
{
    public interface IPopupAnimator
    {
        public event Action OnShowStarted;
        public event Action OnShowFinished;
        public event Action OnHideStarted;
        public event Action OnHideFinished;

        public void Show();
        public void Hide();
    }
    
    [RequireComponent(typeof(Animator))]
    [DisallowMultipleComponent]
    public class PopupAnimator : MonoBehaviour, IPopupAnimator
    {
        private const string NAME_SHOW = "Show";
        private const string NAME_HIDE = "Hide";

        [SerializeField] private Animator animator;

        public event Action OnShowStarted;
        public event Action OnShowFinished;
        public event Action OnHideStarted;
        public event Action OnHideFinished;

        private void Start()
        {
            if (animator == null)
                animator = GetComponent<Animator>();
        }

        public void Show()
        {
            this.OnShowStarted?.Invoke();
            this.animator.Play(NAME_SHOW, -1, 0);
        }

        public void Hide()
        {
            this.OnHideStarted?.Invoke();
            this.animator.Play(NAME_HIDE, -1, 0);
        }

        [UsedImplicitly]
        private void OnShown()
        {
            this.OnShowFinished?.Invoke();
        }

        [UsedImplicitly]
        private void OnHidden()
        {
            this.OnHideFinished?.Invoke();
        }


#if UNITY_EDITOR
        private void Reset()
        {
            this.animator = this.GetComponent<Animator>();
        }
#endif
    }
}