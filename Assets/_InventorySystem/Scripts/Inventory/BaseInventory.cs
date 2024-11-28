// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-28
// <file>: BaseInventory.cs
// ------------------------------------------------------------------------------

using System;
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
        void DecrementItem(InventoryItem inventoryItem, int decrementItem);
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
        private readonly ListInventory _listInventory;
        
        private readonly InventoryItemAdder _adder;
        private readonly InventoryItemStacker _stacker;
        private readonly InventoryItemRemover _remover;
        private readonly InventoryItemFinder _finder;

        public BaseInventory()
        {
            _listInventory = new ListInventory();
            _adder = new InventoryItemAdder(_listInventory);
            _stacker = new InventoryItemStacker();
            _remover = new InventoryItemRemover(_listInventory);
            _finder = new InventoryItemFinder(_listInventory);
        }

        public void AddItem(InventoryItem inventoryItem)
        {
            if (_finder.TryFindItem(inventoryItem, out var foundItem))
            {
                if (_stacker.TryIncrement(foundItem, inventoryItem))
                {
                    OnItemStackChanged?.Invoke(foundItem);
                }
                
                return;
            }

            _adder.Add(inventoryItem);
            OnItemAdded?.Invoke(inventoryItem);
            Log($"Item with ID '{inventoryItem.Id}' added successfully.");
        }
        
        public void ConsumeItem(InventoryItem inventoryItem)
        {
            if (!inventoryItem.FlagsExists(InventoryItemFlags.CONSUMABLE))
            {
                Log("smth");
                return;
            }

            if (!_finder.TryFindItem(inventoryItem, out var foundItem))
            {
                Log($"Item with ID '{inventoryItem.Id}' NOT found. Cannot consume.");
                return;
            }

            if (!foundItem.TryGetComponent<IInventoryItemComponentConsumable>(out var componentConsumable))
            {
                Log("smth");
                return;
            }
            
            DecrementItem(foundItem, componentConsumable.ConsumeAmount);
            OnItemConsumed?.Invoke(foundItem);
        }

        public void DecrementItem(InventoryItem inventoryItem, int decrementValue)
        {
            if (!_finder.TryFindItem(inventoryItem, out var foundItem))
            {
                Log($"Item with ID '{inventoryItem.Id}' NOT found. Cannot remove.");
                return;
            }
            
            if (_stacker.TryDecrement(foundItem, decrementValue))
            {
                Log($"Item with ID '{foundItem.Id}' decremented successfully.");
                OnItemStackChanged?.Invoke(foundItem);
                return;
            }
            
            Remove(foundItem);
        }

        public void RemoveItem(InventoryItem inventoryItem, bool removeAllStack)
        {
            if (inventoryItem.FlagsExists(InventoryItemFlags.STACKABLE) && !removeAllStack)
            {
                DecrementItem(inventoryItem, 1);
                return;
            }
            
            if (!_finder.TryFindItem(inventoryItem, out var foundItem))
            {
                Log($"Item with ID '{inventoryItem.Id}' NOT found. Cannot remove.");
                return;
            }

            Remove(foundItem);
        }

        private void Remove(InventoryItem removeItem)
        {
            _remover.Remove(removeItem);
            OnItemRemoved?.Invoke(removeItem);
            Log($"Item with ID '{removeItem.Id}' removed successfully.");
        }

        private void Log(string message)
        {
            Debug.Log(message);
        }
    }
}