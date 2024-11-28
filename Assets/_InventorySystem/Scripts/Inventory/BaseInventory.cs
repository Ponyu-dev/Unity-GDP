// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-28
// <file>: BaseInventory.cs
// ------------------------------------------------------------------------------

using System;
using _InventorySystem.Scripts.Inventory.InventoryOperations;
using _InventorySystem.Scripts.Item;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _InventorySystem.Scripts.Inventory
{
    public interface IBaseInventory
    {
        event Action<InventoryItem> OnItemAdded;
        event Action<InventoryItem> OnItemStackChanged;
        event Action<InventoryItem> OnItemRemoved;
        
        void AddItem(InventoryItem inventoryItem);
        void RemoveItem(InventoryItem inventoryItem);
    }
    
    [Serializable]
    public sealed class BaseInventory : IBaseInventory
    {
        public event Action<InventoryItem> OnItemAdded;
        public event Action<InventoryItem> OnItemStackChanged;
        public event Action<InventoryItem> OnItemRemoved;

        [ReadOnly, ShowInInspector]
        private readonly ListInventory _listInventory;
        
        private readonly InventoryItemAdder _adder;
        private readonly InventoryItemStacker _stacker;
        private readonly InventoryItemRemover _remover;
        private readonly InventoryItemFinder _finder;

        public BaseInventory()
        {
            _listInventory = new ListInventory();
            _adder = new InventoryItemAdder(_listInventory);
            _stacker = new InventoryItemStacker(_listInventory);
            _remover = new InventoryItemRemover(_listInventory);
            _finder = new InventoryItemFinder(_listInventory);
        }

        public void AddItem(InventoryItem inventoryItem)
        {
            if (_finder.TryFindItem(inventoryItem, out var foundItem))
            {
                if (_stacker.TryIncrement(foundItem, inventoryItem))
                {
                    Log($"Item with ID '{foundItem.Id}' incremented successfully.");
                    OnItemStackChanged?.Invoke(foundItem);
                }
                
                Log($"Item with ID '{foundItem.Id}' found. Cannot add.");
                return;
            }

            _adder.Add(inventoryItem);
            OnItemAdded?.Invoke(inventoryItem);
            Log($"Item with ID '{inventoryItem.Id}' added successfully.");
        }

        public void RemoveItem(InventoryItem inventoryItem)
        {
            if (!_finder.TryFindItem(inventoryItem, out var foundItem))
            {
                Log($"Item with ID '{inventoryItem.Id}' NOT found. Cannot remove.");
                return;
            }

            if (_stacker.TryDecrement(foundItem, inventoryItem))
            {
                Log($"Item with ID '{foundItem.Id}' decremented successfully.");
                OnItemStackChanged?.Invoke(foundItem);
                return;
            }
            
            _remover.Remove(foundItem);
            OnItemRemoved?.Invoke(foundItem);
            Log($"Item with ID '{foundItem.Id}' removed successfully.");
        }

        private void Log(string message)
        {
            Debug.Log(message);
        }
    }
}