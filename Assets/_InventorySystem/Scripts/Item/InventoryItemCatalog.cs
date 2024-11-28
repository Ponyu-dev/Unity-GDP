// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-27
// <file>: InventoryItemCatalog.cs
// ------------------------------------------------------------------------------

using System;
using UnityEngine;

namespace _InventorySystem.Scripts.Item
{
    [CreateAssetMenu(
        fileName = "InventoryItemCatalog",
        menuName = "Inventory/New InventoryItemCatalog"
    )]
    public sealed class InventoryItemCatalog : ScriptableObject
    {
        [SerializeField]
        private InventoryItemConfig[] items;

        public InventoryItemConfig FindItem(string id)
        {
            for (int i = 0, count = items.Length; i < count; i++)
            {
                var item = items[i];
                if (item.ItemName == id)
                {
                    return item;
                }
            }

            throw new Exception($"Item {name} is not found!");
        }

        public InventoryItemConfig[] GetAllItems()
        {
            return items;
        }
    }
}