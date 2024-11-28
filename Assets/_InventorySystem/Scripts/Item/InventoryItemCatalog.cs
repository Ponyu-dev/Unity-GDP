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
        /*[SerializeField]
        private InventoryItemConfig[] items;

        public InventoryItemConfig[] GetAllItems()
        {
            return items;
        }*/
    }
}