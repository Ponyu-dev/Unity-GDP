// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-27
// <file>: InventoryItemConfig.cs
// ------------------------------------------------------------------------------

using Sirenix.OdinInspector;
using UnityEngine;

namespace _InventorySystem.Scripts.Item
{
    [CreateAssetMenu(
        fileName = "InventoryItemConfig",
        menuName = "Inventory/New InventoryItem"
    )]
    public sealed class InventoryItemConfig : SerializedScriptableObject
    {
        public InventoryItem Clone => origin.Clone();
        
        [SerializeField] private InventoryItem origin;
    }
}