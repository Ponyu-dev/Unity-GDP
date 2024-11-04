using System;
using Popups.Helpers;
using UnityEngine;

namespace Popups
{
    public interface IPopupPresenter : IPopupAnimatorCallback, IPopupClickCallback
    {
        event Action<Type, PopupView, IPopupPresenter> EventHideFinished;
        void Init(Type popupType, PopupView popupView, PopupData data);
        void Show();
    }
    
    public abstract class PopupPresenter : IPopupPresenter, IDisposable
    {
        protected Type PresenterType;
        protected PopupView PopupView;
        protected IPopupAnimatorView PopupAnimatorView;

        public event Action<Type, PopupView, IPopupPresenter> EventHideFinished;

        public virtual void Init(Type presenterType, PopupView popupView, PopupData data)
        {
            PresenterType = presenterType;
            PopupView = popupView;
            PopupView.Constructor(this);
            PopupAnimatorView = popupView.PopupAnimator;
            PopupAnimatorView.Constructor(this);
        }
        
        public virtual void Show() => PopupAnimatorView.Show();
        
        public virtual void OnApplyClicked() => PopupAnimatorView.Hide();
        public virtual void OnCancelClicked() => PopupAnimatorView.Hide();
        public virtual void OnCloseClicked() => PopupAnimatorView.Hide();
        
        public virtual void OnShowStarted() { }
        public virtual void OnShowFinished() { }
        public virtual void OnHideStarted() { }

        public virtual void OnHideFinished()
        {
            Debug.Log("[PopupPresenter] OnHideFinished and Object Destroy");
            EventHideFinished?.Invoke(PresenterType, PopupView, this);
        }

        public virtual void Dispose()
        {
            Debug.Log("[PopupPresenter] Dispose");
        }
    }
}