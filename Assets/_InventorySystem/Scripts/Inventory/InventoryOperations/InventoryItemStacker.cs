// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-28
// <file>: InventoryItemStacker.cs
// ------------------------------------------------------------------------------

using _InventorySystem.Scripts.Extensions;
using _InventorySystem.Scripts.Item;
using _InventorySystem.Scripts.Item.Components;
using UnityEngine;

namespace _InventorySystem.Scripts.Inventory.InventoryOperations
{
    public sealed class InventoryItemStacker
    {
        private readonly ListInventory _listInventory;
        
        public InventoryItemStacker(ListInventory listInventory)
        {
            _listInventory = listInventory;
        }

        public bool TryIncrement(InventoryItem targetItem, InventoryItem stackItem)
        {
            if (!HasStackableItem(targetItem, out var targetItemComponent))
                return false;
            
            if (!HasStackableItem(stackItem, out var stackItemComponent))
                return false;
            
            targetItemComponent.Increment(stackItemComponent.Count);
            
            Debug.Log($"Successfully stacked {stackItemComponent.Count} items from stackItem into target item {targetItem.Id}.");
            
            return true;
        }

        public bool TryDecrement(InventoryItem targetItem, InventoryItem decrementItem)
        {
            if (!HasStackableItem(targetItem, out var targetComponent))
            {
                return false;
            }
            
            if (!HasStackableItem(decrementItem, out var decrementComponent))
            {
                return false;
            }
            
            targetComponent.Decrement(decrementComponent.Count);

            return targetComponent.IsNotEmpty();
        }

        private bool HasStackableItem(InventoryItem item, out IInventoryItemComponentStackable component)
        {
            if (!item.FlagsExists(InventoryItemFlags.STACKABLE))
            {
                Debug.LogWarning($"Item {item.Id} cannot be stacked because it is not stackable.");
                component = default;
                return false;
            }
            
            if (!item.TryGetComponent(out component))
            {
                Debug.LogWarning($"Item {item.Id} does not have a stackable component.");
                component = default;
                return false;
            }
            
            return true;
        }
    }
}