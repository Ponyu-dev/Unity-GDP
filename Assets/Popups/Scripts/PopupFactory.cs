using System;
using System.Collections.Generic;
using Popups.Helpers;
using UnityEngine;
using VContainer;
using Object = UnityEngine.Object;

namespace Popups
{
    public interface IPopupFactory
    {
        bool CanShowPopup<TView, TPresenter>(PopupData data)
            where TView : PopupView
            where TPresenter : PopupPresenter;
    }

    public class PopupFactory : IPopupFactory
    {
        private readonly PopupCatalog _catalog;
        private readonly Transform _container;
        private readonly IObjectResolver _resolver;
        
        private readonly Queue<(PresenterType, PopupData)> _popupQueue = new();
        private readonly HashSet<PresenterType> _activePopups = new();
        
        [Inject]
        public PopupFactory(
            PopupCatalog catalog,
            Transform container,
            IObjectResolver resolver)
        {
            Debug.Log("[PopupFactory] Constructor");
            _catalog = catalog;
            _container = container;
            _resolver = resolver;
        }
        
        public bool CanShowPopup<TView, TPresenter>(PopupData data) 
            where TView : PopupView
            where TPresenter : PopupPresenter
        {
            var presenterType = new PresenterType(typeof(TPresenter));
            if (_activePopups.Contains(presenterType) || _popupQueue.Contains((presenterType, data)))
            {
                Debug.LogWarning($"[PopupFactory] Popup {presenterType.Type} is already shown or in queue.");
                presenterType = null;
                return false;
            }
            
            if (_activePopups.Count > 0)
            {
                _popupQueue.Enqueue((presenterType, data));
                return false;
            }
            
            ShowPopup<TView, TPresenter>(data, presenterType);
            return true;
        }
        
        private void ShowPopup<TView, TPresenter>(PopupData data, PresenterType presenterType)
            where TView : PopupView
            where TPresenter : PopupPresenter
        {
            var popup = _catalog.GetPopup(presenterType);
            var prefab = Object.Instantiate(popup.prefab, _container);

            if (!prefab.TryGetComponent<TView>(out var view))
            {
                Debug.LogError($"[PopupFactory] ShowPopup {typeof(TView).Name} cannot be found on prefab.");
                return;
            }

            if (!_resolver.TryResolve<TPresenter>(out var presenter))
            {
                Debug.LogError($"[PopupFactory] ShowPopup {typeof(TPresenter).Name} cannot resolve for VContainer.");
                return;
            }
            
            presenter.Init(presenterType, view, data);
            presenter.Show();
            
            _activePopups.Add(presenterType);
            
            presenter.EventHideFinished += OnPopupClose;
        }
        
        private void OnPopupClose(PresenterType type, PopupView view, IPopupPresenter presenter)
        {
            presenter.EventHideFinished -= OnPopupClose;
            Object.Destroy(view.gameObject);
            _activePopups.Remove(type);
            if (_popupQueue.Count <= 0) return;
            
            var (nextPopupType, nextData) = _popupQueue.Dequeue();
            ShowPopup<PopupView, PopupPresenter>(nextData, nextPopupType);
        }
    }
}