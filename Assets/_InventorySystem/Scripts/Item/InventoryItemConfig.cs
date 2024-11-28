// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-27
// <file>: InventoryItemConfig.cs
// ------------------------------------------------------------------------------

using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace _InventorySystem.Scripts.Item
{
    [CreateAssetMenu(
        fileName = "InventoryItemConfig",
        menuName = "Inventory/New InventoryItem"
    )]
    public sealed class InventoryItemConfig : SerializedScriptableObject
    {
        public string ItemName => origin.Id;
        public InventoryItemFlags Flags => origin.Flags;
        public InventoryItemMetadata Metadata => origin.Metadata;

        public InventoryItem Clone => origin.Clone();
        
        [OdinSerialize] private InventoryItem origin = new();
    }
}