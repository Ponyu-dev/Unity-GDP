using System;
using System.Collections.Generic;
using UnityEngine;

namespace Popups
{
    [CreateAssetMenu(menuName = "Popups/New PopupCatalog", fileName = "PopupCatalog")]
    public class PopupCatalog : ScriptableObject
    {
        [SerializeField] private List<PopupSO> popupEntries;
        
        public PopupSO GetPopup(PopupType type)
        {
            throw new Exception($"Prefab {name} is not found!");
        }
    }
}