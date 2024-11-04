using System;
using System.Collections.Generic;
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

    public class PopupQueueItem
    {
        public PopupData PopupData { get; }
        public Type TView { get; }
        public Type TPresenter { get; }

        public PopupQueueItem(Type presenterType, Type tView, PopupData popupData)
        {
            TPresenter = presenterType;
            TView = tView;
            PopupData = popupData;
        }
    }

    public class PopupFactory : IPopupFactory
    {
        private readonly PopupCatalog _catalog;
        private readonly Transform _container;
        private readonly IObjectResolver _resolver;
        
        private readonly Queue<PopupQueueItem> _popupQueue = new();
        private readonly HashSet<Type> _activePopups = new();
        
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
            if (_catalog.CatalogIsEmpty)
            {
                Debug.LogWarning($"[PopupFactory] The popup catalog is empty. Please add the required PopupSO to the catalog.");
                return false;
            }
            
            var presenterType = typeof(TPresenter);
            var viewType = typeof(TView);
            
            var popupItem = new PopupQueueItem(presenterType, viewType, data);
            
            if (_activePopups.Contains(presenterType) || _popupQueue.Contains(popupItem))
            {
                Debug.LogWarning($"[PopupFactory] Popup {presenterType.Name} is already shown or in queue.");
                return false;
            }
            
            if (_activePopups.Count > 0)
            {
                _popupQueue.Enqueue(popupItem);
                return false;
            }
            
            ShowPopup(popupItem);
            return true;
        }
        
        private void ShowPopup(PopupQueueItem popupItem)
        {
            var popup = _catalog.GetPopup(popupItem.TPresenter);
            var prefab = Object.Instantiate(popup.prefab, _container).gameObject;

            if (!TryGetComponents(prefab, popupItem, out var view, out var presenter))
            {
                return;
            }

            presenter.Init(popupItem.TPresenter, view, popupItem.PopupData);
            presenter.Show();

            _activePopups.Add(popupItem.TPresenter);
            presenter.EventHideFinished += OnPopupClose;
        }

        private bool TryGetComponents(GameObject prefab, PopupQueueItem popupItem, out PopupView view, out PopupPresenter presenter)
        {
            view = null;
            presenter = null;

            if (!prefab.TryGetComponent(popupItem.TView, out var prefabView))
            {
                Debug.LogError($"[PopupFactory] ShowPopup: Unable to find component {popupItem.TView.Name} on prefab {prefab.name}.");
                return false;
            }

            if (!_resolver.TryResolve(popupItem.TPresenter, out var pres))
            {
                Debug.LogError($"[PopupFactory] ShowPopup: Unable to resolve presenter {popupItem.TPresenter.Name} for VContainer. Make sure it is registered in the container.");
                return false;
            }

            if (pres is not PopupPresenter foundPresenter)
            {
                Debug.LogError($"[PopupFactory] ShowPopup: Resolved object is not of type {nameof(PopupPresenter)}. Obtained: {pres.GetType().Name}.");
                return false;
            }

            if (prefabView is not PopupView foundView)
            {
                Debug.LogError($"[PopupFactory] ShowPopup: Resolved view is not of type {nameof(PopupView)}. Obtained: {prefabView.GetType().Name}.");
                return false;
            }

            view = foundView;
            presenter = foundPresenter;
            return true;
        }
        
        private void OnPopupClose(Type type, PopupView view, IPopupPresenter presenter)
        {
            presenter.EventHideFinished -= OnPopupClose;
            Object.Destroy(view.gameObject);
            _activePopups.Remove(type);
            if (_popupQueue.Count <= 0) return;
            
            var nextPopupItem = _popupQueue.Dequeue();
            ShowPopup(nextPopupItem);
        }
    }
}