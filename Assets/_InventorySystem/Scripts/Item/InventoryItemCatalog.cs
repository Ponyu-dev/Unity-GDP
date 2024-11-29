// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-27
// <file>: InventoryItemCatalog.cs
// ------------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace _InventorySystem.Scripts.Item
{
    [CreateAssetMenu(
        fileName = "InventoryItemCatalogConfig",
        menuName = "Inventory/New InventoryItemCatalogConfig"
    )]
    public sealed class InventoryItemCatalog : ScriptableObject
    {
        [SerializeField]
        private List<InventoryItemConfig> items;

        public IReadOnlyList<InventoryItemConfig> GetAllItems()
        {
            return items;
        }
    }
}