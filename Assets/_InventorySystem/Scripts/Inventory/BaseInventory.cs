// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-28
// <file>: BaseInventory.cs
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using _InventorySystem.Scripts.Item;
using Sirenix.OdinInspector;

namespace _InventorySystem.Scripts.Inventory
{
    public interface IBaseInventory
    {
        event Action<InventoryItem> OnItemAdded;
        event Action<InventoryItem> OnItemStackChanged;
        event Action<InventoryItem> OnItemRemoved;

        void AddItem(InventoryItem inventoryItem);
        void RemoveItem(InventoryItem inventoryItem, bool removeAllStack = false);
    }
    
    [Serializable]
    public sealed class BaseInventory : IBaseInventory
    {
        public event Action<InventoryItem> OnItemAdded;
        public event Action<InventoryItem> OnItemStackChanged;
        public event Action<InventoryItem> OnItemRemoved;

        [ReadOnly, ShowInInspector]
        private readonly List<InventoryItem> _items;
        public IReadOnlyList<InventoryItem> Items => _items;

        public BaseInventory()
        {
            _items = new List<InventoryItem>();
        }

        public void AddItem(InventoryItem inventoryItem)
        {
            if (inventoryItem is null)
                return;
            
            var item = _items.Find(it => it.Id == inventoryItem.Id);
            if (item is not null)
            {
                item.TryIncrement(inventoryItem);
                OnItemStackChanged?.Invoke(item);
                return;
            }
            
            _items.Add(inventoryItem);
            OnItemAdded?.Invoke(inventoryItem);
        }

        public void RemoveItem(InventoryItem inventoryItem, bool removeAllStack = false)
        {
            if (inventoryItem.FlagsExists(InventoryItemFlags.STACKABLE) && !removeAllStack)
            {
                DecrementItem(inventoryItem, 1);
                return;
            }

            RemoveAt(inventoryItem);
        }
        
        private void DecrementItem(InventoryItem findItem, int decrementValue)
        {
            var item = _items.Find(it => it.Id == findItem.Id);
            
            if (item.TryDecrement(decrementValue))
            {
                OnItemStackChanged?.Invoke(item);
                return;
            }
            
            RemoveAt(item);
        }

        private void RemoveAt(InventoryItem removeItem)
        {
            var index = _items.FindIndex(it => it.Id == removeItem.Id);
            
            if (index < 0)
                return;
            
            _items.RemoveAt(index);
            OnItemRemoved?.Invoke(removeItem);
        }
    }
}