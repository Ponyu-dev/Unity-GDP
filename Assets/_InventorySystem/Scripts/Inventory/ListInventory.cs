// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-28
// <file>: ListInventory.cs
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using _InventorySystem.Scripts.Item;
using Sirenix.OdinInspector;

namespace _InventorySystem.Scripts.Inventory
{
    [Serializable]
    public sealed class ListInventory
    {
        [ReadOnly, ShowInInspector]
        private readonly List<InventoryItem> _items;
        public IReadOnlyList<InventoryItem> Items => _items;

        public ListInventory()
        {
            _items = new List<InventoryItem>();
        }

        public void AddRange(IEnumerable<InventoryItem> list)
        {
            _items.AddRange(list);
        }

        public void Add(InventoryItem inventoryItem)
        {
            _items.Add(inventoryItem);
        }

        public bool Remove(InventoryItem inventoryItem)
        {
            return _items.Remove(inventoryItem);
        }
    }
}