using JetBrains.Annotations;
using UnityEngine;

namespace Popups
{
    public interface IPopupAnimatorView
    {
        public void Constructor(IPopupAnimatorCallback popupAnimatorCallback);
        public void Show();
        public void Hide();
    }
    
    [RequireComponent(typeof(Animator))]
    [DisallowMultipleComponent]
    public class PopupAnimatorView : MonoBehaviour, IPopupAnimatorView
    {
        private const string NAME_SHOW = "Show";
        private const string NAME_HIDE = "Hide";

        [SerializeField] private Animator animator;

        private IPopupAnimatorCallback _popupAnimatorCallback;

        public void Constructor(IPopupAnimatorCallback popupAnimatorCallback)
        {
            _popupAnimatorCallback = popupAnimatorCallback;
        }

        private void Start()
        {
            if (animator == null)
                animator = GetComponent<Animator>();
        }

        public void Show()
        {
            _popupAnimatorCallback?.OnShowStarted();
            animator.Play(NAME_SHOW, -1, 0);
        }

        public void Hide()
        {
            _popupAnimatorCallback?.OnHideStarted();
            animator.Play(NAME_HIDE, -1, 0);
        }

        [UsedImplicitly]
        private void OnShown()
        {
            _popupAnimatorCallback?.OnShowFinished();
        }

        [UsedImplicitly]
        private void OnHidden()
        {
            _popupAnimatorCallback?.OnHideFinished();
        }

#if UNITY_EDITOR
        private void Reset()
        {
            animator = GetComponent<Animator>();
        }
#endif
    }
}