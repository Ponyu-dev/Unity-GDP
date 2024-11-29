// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-27
// <file>: InventoryItemExtensions.cs
// ------------------------------------------------------------------------------

using _InventorySystem.Scripts.Item.Components;

namespace _InventorySystem.Scripts.Item
{
    public static class InventoryItemExtensions
    {
        public static bool FlagsExists(this InventoryItem it, InventoryItemFlags flags)
        {
            return (it?.Flags & flags) == flags;
        }
        
        public static bool TryGetComponentSafe<T>(
            this InventoryItem item,
            InventoryItemFlags requiredFlag,
            out T component
            ) where T : class
        {
            component = default;
            
            if (item is null)
                return false;
            
            return item.FlagsExists(requiredFlag) && item.TryGetComponent(out component);
        }
        
        public static bool TryIncrement(this InventoryItem targetItem, InventoryItem stackItem)
        {
            if (targetItem is null || stackItem is null)
                return false;
            
            if (!targetItem.TryGetComponentSafe<IInventoryItemComponentStackable>(InventoryItemFlags.STACKABLE, out var targetComponent))
                return false;

            if (!stackItem.TryGetComponentSafe<IInventoryItemComponentStackable>(InventoryItemFlags.STACKABLE, out var stackComponent))
                return false;

            targetComponent.Increment(stackComponent.Count);
            return true;
        }

        public static bool TryDecrement(this InventoryItem targetItem, int decrementValue)
        {
            if (targetItem is null)
                return false;
            
            if (!targetItem.TryGetComponentSafe<IInventoryItemComponentStackable>(InventoryItemFlags.STACKABLE, out var stackableComponent))
                return false;
            
            stackableComponent.Decrement(decrementValue);
            return stackableComponent.IsNotEmpty();
        }
    }
}