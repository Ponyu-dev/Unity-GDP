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
    }
}