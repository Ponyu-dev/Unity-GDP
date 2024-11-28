// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-28
// <file>: BaseInventory.cs
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using _InventorySystem.Scripts.Extensions;
using _InventorySystem.Scripts.Inventory.InventoryOperations;
using _InventorySystem.Scripts.Item;
using _InventorySystem.Scripts.Item.Components;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _InventorySystem.Scripts.Inventory
{
    public interface IBaseInventory
    {
        event Action<InventoryItem> OnItemAdded;
        event Action<InventoryItem> OnItemStackChanged;
        event Action<InventoryItem> OnItemConsumed;
        event Action<InventoryItem> OnItemRemoved;
        
        void AddItem(InventoryItem inventoryItem);
        void ConsumeItem(InventoryItem inventoryItem);
        void EquipItem(InventoryItem inventoryItem);
        void UnEquipItem(EquipmentSlot unEquipSlot);
        void RemoveItem(InventoryItem inventoryItem, bool removeAllStack);
    }
    
    [Serializable]
    public sealed class BaseInventory : IBaseInventory
    {
        public event Action<InventoryItem> OnItemAdded;
        public event Action<InventoryItem> OnItemStackChanged;
        public event Action<InventoryItem> OnItemConsumed;
        public event Action<InventoryItem> OnItemRemoved;

        [ReadOnly, ShowInInspector]
        private readonly List<InventoryItem> _items;
        public IReadOnlyList<InventoryItem> Items => _items;
        
        [ReadOnly, ShowInInspector] private readonly EquipInventory _equipInventory;
        
        private readonly InventoryItemStacker _stacker;

        public BaseInventory()
        {
            _items = new List<InventoryItem>();
            _equipInventory = new EquipInventory();
            _stacker = new InventoryItemStacker();
        }

        public void AddItem(InventoryItem inventoryItem)
        {
            if (inventoryItem is null)
                return;
            
            var item = _items.Find(it => it.Id == inventoryItem.Id);
            if (item is not null)
            {
                _stacker.TryIncrement(item, inventoryItem);
                OnItemStackChanged?.Invoke(item);
                return;
            }
            
            _items.Add(inventoryItem);
            OnItemAdded?.Invoke(inventoryItem);
        }
        
        public void ConsumeItem(InventoryItem inventoryItem)
        {
            var item = _items.Find(it => it.Id == inventoryItem.Id);
            
            if (item is null)
            {
                Log($"Item with ID '{inventoryItem.Id}' NOT found. Cannot consume.");
                return;
            }

            if (!inventoryItem.TryGetComponentSafe<IInventoryItemComponentConsumable>(InventoryItemFlags.CONSUMABLE,
                    out var componentConsumable))
            {
                return;
            }
            
            DecrementItem(item, componentConsumable.ConsumeAmount);
            OnItemConsumed?.Invoke(item);
        }

        public void EquipItem(InventoryItem inventoryItem)
        {
            if (!_equipInventory.EquipItem(inventoryItem, out var oldEquipItem))
                return;
            
            RemoveAt(inventoryItem);
            AddItem(oldEquipItem);
        }

        public void UnEquipItem(EquipmentSlot unEquipSlot)
        {
            if (_equipInventory.TryUnEquipItem(unEquipSlot, out var unEquipItem))
            {
                AddItem(unEquipItem);
            }
        }

        public void RemoveItem(InventoryItem inventoryItem, bool removeAllStack = false)
        {
            if (inventoryItem == null)
            {
                return;
            }
            
            if (inventoryItem.FlagsExists(InventoryItemFlags.STACKABLE) && !removeAllStack)
            {
                DecrementItem(inventoryItem, 1);
                return;
            }

            RemoveAt(inventoryItem);
        }
        
        private void DecrementItem(InventoryItem item, int decrementValue)
        {
            if (_stacker.TryDecrement(item, decrementValue))
            {
                Log($"Item with ID '{item.Id}' decremented successfully.");
                OnItemStackChanged?.Invoke(item);
                return;
            }
            
            RemoveAt(item);
        }

        private void RemoveAt(InventoryItem removeItem)
        {
            var index = _items.FindIndex(it => it.Id == removeItem.Id);
            _items.RemoveAt(index);
            OnItemRemoved?.Invoke(removeItem);
            Log($"Item with ID '{removeItem.Id}' removed successfully.");
        }

        private void Log(string message)
        {
            Debug.Log(message);
        }
    }
}