// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-27
// <file>: InventoryItemExtensions.cs
// ------------------------------------------------------------------------------

using _InventorySystem.Scripts.Item;

namespace _InventorySystem.Scripts.Extensions
{
    public static class InventoryItemExtensions
    {
        public static bool FlagsExists(this InventoryItem it, InventoryItemFlags flags)
        {
            return (it.Flags & flags) == flags;
        }
        
        public static bool TryGetComponentSafe<T>(
            this InventoryItem item,
            InventoryItemFlags requiredFlag,
            out T component
        ) where T : class
        {
            component = default;
            return item.FlagsExists(requiredFlag) && item.TryGetComponent(out component);
        }
    }
}