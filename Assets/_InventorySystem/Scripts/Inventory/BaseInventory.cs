// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-28
// <file>: BaseInventory.cs
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using _InventorySystem.Scripts.Inventory.InventoryOperations;
using _InventorySystem.Scripts.Item;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _InventorySystem.Scripts.Inventory
{
    public interface IBaseInventory
    {
        event Action<InventoryItem> OnAddedItem;
        event Action<InventoryItem> OnRemovedItem;
        
        void AddItem(InventoryItem inventoryItem);
        void RemoveItem(InventoryItem inventoryItem);
    }
    
    [Serializable]
    public sealed class BaseInventory : IBaseInventory
    {
        public event Action<InventoryItem> OnAddedItem;
        public event Action<InventoryItem> OnRemovedItem;

        [ReadOnly, ShowInInspector]
        private readonly ListInventory _listInventory;
        
        private readonly InventoryItemAdder _adder;
        private readonly InventoryItemRemover _remover;
        private readonly InventoryItemFinder _finder;

        public BaseInventory()
        {
            _listInventory = new ListInventory();
            _adder = new InventoryItemAdder(_listInventory);
            _remover = new InventoryItemRemover(_listInventory);
            _finder = new InventoryItemFinder(_listInventory);
        }

        public void AddItem(InventoryItem inventoryItem)
        {
            if (_finder.TryFindItem(inventoryItem, out var foundItem))
            {
                Log($"Item with ID '{inventoryItem.Id}' found. Cannot add.");
                return;
            }

            _adder.Add(inventoryItem);
            OnAddedItem?.Invoke(inventoryItem);
            //Log($"Item with ID '{foundItem.Id}' added successfully.");
        }

        public void RemoveItem(InventoryItem inventoryItem)
        {
            if (!_finder.TryFindItem(inventoryItem, out var foundItem))
            {
                Log($"Item with ID '{inventoryItem.Id}' NOT found. Cannot remove.");
                return;
            }

            _remover.Remove(foundItem);
            OnRemovedItem?.Invoke(foundItem);
            Log($"Item with ID '{foundItem.Id}' removed successfully.");
        }

        private void Log(string message)
        {
            Debug.Log(message);
        }
    }
}