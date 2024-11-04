using System;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

namespace Popups
{
    [CreateAssetMenu(menuName = "Popups/New PopupCatalog", fileName = "PopupCatalog")]
    public class PopupCatalog : ScriptableObject
    {
        [SerializeField] private List<PopupSOBase> popupEntries;

        public bool CatalogIsEmpty => popupEntries.IsNullOrEmpty();
        
        public PopupSOBase GetPopup(Type presenterType)
        {
            Debug.Log($"GetPopup {presenterType.Name}");
            return popupEntries.Find(it => it.PresenterType == presenterType);
        }
    }
}