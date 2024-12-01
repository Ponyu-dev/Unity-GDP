// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-29
// <file>: ConsumableSystem.cs
// ------------------------------------------------------------------------------

using System;
using _InventorySystem.Scripts.Item;
using _InventorySystem.Scripts.Item.Components;
using UnityEngine;
using VContainer;

namespace _InventorySystem.Scripts.Inventory.System
{
    public interface IConsumableSystemAction
    {
        event Action<InventoryItem> OnConsumeAction;
    }
    
    public interface IConsumableSystem
    {
        void ConsumeItem(InventoryItem inventoryItem);
    }
    
    public sealed class ConsumableSystem : IConsumableSystem, IConsumableSystemAction
    {
        public event Action<InventoryItem> OnConsumeAction;
        private readonly IBaseInventory _baseInventory;

        [Inject]
        public ConsumableSystem(IBaseInventory baseInventory)
        {
            _baseInventory = baseInventory;
        }

        public void ConsumeItem(InventoryItem inventoryItem)
        {
            if (!inventoryItem.TryGetComponentSafe<IInventoryItemComponentConsumable>(InventoryItemFlags.CONSUMABLE, out var consumable))
                return;
            
            Debug.Log($"[ConsumableSystem] ConsumeItem {inventoryItem.Id}");
            OnConsumeAction?.Invoke(inventoryItem);
            _baseInventory.RemoveItem(inventoryItem);
        }
    }
}