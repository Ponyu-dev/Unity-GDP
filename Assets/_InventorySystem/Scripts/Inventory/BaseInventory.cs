// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-28
// <file>: BaseInventory.cs
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using _InventorySystem.Scripts.Item;
using Sirenix.OdinInspector;
using VContainer;

namespace _InventorySystem.Scripts.Inventory
{
    public interface IBaseInventory
    {
        event Action<InventoryItem> OnItemAdded;
        event Action<InventoryItem> OnItemStackChanged;
        event Action<InventoryItem> OnItemRemoved;
        
        IReadOnlyList<InventoryItem> Items { get; }

        void AddItem(InventoryItem inventoryItem);
        void RemoveItem(InventoryItem inventoryItem, bool removeAllStack = false);
    }
    
    [Serializable]
    public sealed class BaseInventory : IBaseInventory, IDisposable
    {
        public event Action<InventoryItem> OnItemAdded;
        public event Action<InventoryItem> OnItemStackChanged;
        public event Action<InventoryItem> OnItemRemoved;

        [ReadOnly, ShowInInspector]
        private readonly List<InventoryItem> _items;
        public IReadOnlyList<InventoryItem> Items => _items;

        [Inject]
        public BaseInventory()
        {
            _items = new List<InventoryItem>();
        }

        public void AddItem(InventoryItem inventoryItem)
        {
            if (inventoryItem is null)
                return;

            if (TryIncrementItem(inventoryItem))
                return;

            _items.Add(inventoryItem);
            OnItemAdded?.Invoke(inventoryItem);
        }
        

        public void RemoveItem(InventoryItem inventoryItem, bool removeAllStack = false)
        {
            if (TryDecrementItem(inventoryItem, 1) && !removeAllStack)
                return;

            RemoveAt(inventoryItem);
        }

        private bool TryIncrementItem(InventoryItem inventoryItem)
        {
            if (!inventoryItem.FlagsExists(InventoryItemFlags.STACKABLE))
                return false;
            
            var item = _items.Find(it => it.Id == inventoryItem.Id);
            if (item is null)
                return false;

            if (!item.TryIncrement(inventoryItem))
                return false;
            
            OnItemStackChanged?.Invoke(item);
            return true;
        }
        
        private bool TryDecrementItem(InventoryItem findItem, int decrementValue)
        {
            if (!findItem.FlagsExists(InventoryItemFlags.STACKABLE))
                return false;
            
            var item = _items.Find(it => it.Id == findItem.Id);
            
            if (item is null)
                return false;
            
            if (!item.TryDecrement(decrementValue))
                return false;
            
            OnItemStackChanged?.Invoke(item);
            return true;
        }

        private void RemoveAt(InventoryItem removeItem)
        {
            var index = _items.FindIndex(it => it.InstanceId == removeItem.InstanceId);
            
            if (index < 0)
                return;
            
            _items.RemoveAt(index);
            OnItemRemoved?.Invoke(removeItem);
        }

        public void Dispose()
        {
            _items.Clear();
        }
    }
}