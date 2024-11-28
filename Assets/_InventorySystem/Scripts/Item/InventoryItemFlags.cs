// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-27
// <file>: InventoryItemFlags.cs
// ------------------------------------------------------------------------------

using System;

namespace _InventorySystem.Scripts.Item
{
    [Flags]
    public enum InventoryItemFlags
    {
        NONE = 0,
        STACKABLE = 1,
        CONSUMABLE = 2,
        EQUIPPABLE = 4,
        EFFECTIBLE = 8,
    }
}