using System.Collections.Generic;
using Popups.Helpers;
using UnityEngine;

namespace Popups
{
    [CreateAssetMenu(menuName = "Popups/New PopupCatalog", fileName = "PopupCatalog")]

    public class PopupCatalog : ScriptableObject
    {
        [SerializeField] private List<PopupSO> popupEntries;

        public PopupSO GetPopup(PresenterType presenterType)
        {
            return popupEntries.Find(it => it.presenterType.Equals(presenterType));
        }
    }
}