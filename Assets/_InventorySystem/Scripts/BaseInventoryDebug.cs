// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-28
// <file>: BaseInventoryDebug.cs
// ------------------------------------------------------------------------------

using _InventorySystem.Scripts.Inventory;
using _InventorySystem.Scripts.Item;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _InventorySystem.Scripts
{
    public class BaseInventoryDebug : MonoBehaviour
    {
        [SerializeField] private BaseInventory baseInventory;

        [Button]
        private void AddItem(InventoryItemConfig itemConfig)
        {
            baseInventory.AddItem(itemConfig.Clone);
        }
        
        [Button]
        private void ConsumeItem(InventoryItemConfig itemConfig)
        {
            baseInventory.ConsumeItem(itemConfig.Clone);
        }
        
        [Button]
        private void DecrementItem(InventoryItemConfig itemConfig, int decrementValue)
        {
            baseInventory.DecrementItem(itemConfig.Clone, decrementValue);
        }
        
        [Button]
        private void RemoveItem(InventoryItemConfig itemConfig, bool removeAllStack)
        {
            baseInventory.RemoveItem(itemConfig.Clone, removeAllStack);
        }
    }
}