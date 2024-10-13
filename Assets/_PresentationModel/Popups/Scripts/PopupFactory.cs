using UnityEngine;
using VContainer;

namespace Popups
{
    public interface IPopupFactory
    {
        IPopupPresenter CreatePopup(PopupType key);
    }

    public class PopupFactory : IPopupFactory
    {
        private readonly PopupCatalog _catalog;
        private readonly Transform _container;

        [Inject]
        public PopupFactory(PopupCatalog catalog, Transform container)
        {
            this._catalog = catalog;
            this._container = container;
        }
        
        //TODO надо разобраться.
        public IPopupPresenter CreatePopup(PopupType key)
        {
            var popup = _catalog.GetPopup(key);
            var prefab = Object.Instantiate(popup.prefab, this._container);
            //return prefab;
            return default;
        }
    }
}