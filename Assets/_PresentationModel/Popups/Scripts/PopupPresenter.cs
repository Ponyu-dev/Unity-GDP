using System;
using VContainer;

namespace Popups
{
    public interface IPopupPresenter
    {
        public void Show();
    }
    
    public abstract class PopupPresenter : IPopupPresenter, IDisposable
    {
        protected readonly IPopup _popup;
        private readonly IPopupAnimator _popupAnimator;

        [Inject]
        protected PopupPresenter(
            IPopup popup,
            IPopupAnimator popupAnimator)
        {
            _popup = popup;
            _popup.OnApplyClicked += OnApplyClicked;
            _popup.OnCancelClicked += OnCancelClicked;
            _popup.OnCloseClicked += OnCloseClicked;
            
            _popupAnimator = popupAnimator;
            _popupAnimator.OnShowStarted += OnShowStarted;
            _popupAnimator.OnShowFinished += OnShowFinished;
            _popupAnimator.OnHideStarted += OnHideStarted;
            _popupAnimator.OnHideFinished += OnHideFinished;
        }

        protected virtual void OnApplyClicked()
        {
            _popupAnimator.Hide();
        }

        protected virtual void OnCancelClicked()
        {
            _popupAnimator.Hide();
        }

        protected virtual void OnCloseClicked()
        {
            _popupAnimator.Hide();
        }

        protected virtual void OnShowStarted()
        {
            
        }

        protected virtual void OnShowFinished()
        {
            
        }

        protected virtual void OnHideStarted()
        {
            
        }

        protected virtual void OnHideFinished()
        {
            
        }

        public virtual void Show()
        {
            _popupAnimator.Show();
        }
        
        public void Dispose()
        {
            
        }
    }
}