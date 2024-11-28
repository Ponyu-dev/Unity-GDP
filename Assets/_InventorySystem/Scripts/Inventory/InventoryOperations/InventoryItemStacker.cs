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
        public bool TryIncrement(InventoryItem targetItem, InventoryItem stackItem)
        {
            if (!targetItem.TryGetComponentSafe<IInventoryItemComponentStackable>(InventoryItemFlags.STACKABLE, out var targetItemComponent))
                return false;
            
            if (!stackItem.TryGetComponentSafe<IInventoryItemComponentStackable>(InventoryItemFlags.STACKABLE, out var stackItemComponent))
                return false;
            
            targetItemComponent.Increment(stackItemComponent.Count);
            
            Debug.Log($"Successfully stacked {stackItemComponent.Count} items from stackItem into target item {targetItem.Id}.");
            
            return true;
        }

        public bool TryDecrement(InventoryItem targetItem, int decrementValue)
        {
            if (!targetItem.TryGetComponentSafe<IInventoryItemComponentStackable>(InventoryItemFlags.STACKABLE, out var targetComponent))
            {
                return false;
            }
            
            targetComponent.Decrement(decrementValue);

            return targetComponent.IsNotEmpty();
        }
    }
}