using UnityEngine;

namespace Popups
{
    public interface IPopupView
    {
        IPopupAnimatorView PopupAnimator { get; }
        void Constructor(IPopupClickCallback popupClickCallback);
    }
    
    [RequireComponent(typeof(PopupAnimatorView))]
    public abstract class PopupView : MonoBehaviour, IPopupView
    {
        protected IPopupClickCallback _popupClickCallback;
        private IPopupAnimatorView _popupAnimatorView;

        public IPopupAnimatorView PopupAnimator => _popupAnimatorView;

        private void Awake()
        {
            if (_popupAnimatorView == null && gameObject.TryGetComponent<PopupAnimatorView>(out var animator))
            {
                _popupAnimatorView = animator;
            }
        }

        public void Constructor(IPopupClickCallback popupClickCallback)
        {
            _popupClickCallback = popupClickCallback;
        }

        protected virtual void OnEnable()
        {
            SetupButtons();
        }

        protected virtual void OnDisable()
        {
            RemoveButtonListeners();
        }
        
        protected abstract void SetupButtons();
        protected abstract void RemoveButtonListeners();
    }
}