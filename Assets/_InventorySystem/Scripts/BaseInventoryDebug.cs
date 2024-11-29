// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-28
// <file>: BaseInventoryDebug.cs
// ------------------------------------------------------------------------------

using _InventorySystem.Scripts.Inventory;
using _InventorySystem.Scripts.Inventory.System;
using _InventorySystem.Scripts.Item;
using _InventorySystem.Scripts.Item.Components;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _InventorySystem.Scripts
{
    public class BaseInventoryDebug : MonoBehaviour
    {
        [SerializeField] private BaseInventory baseInventory;
        [SerializeField] private EquipInventory equipInventory;
        
        private EquipmentSystem _equipmentSystem;
        private ConsumableSystem _consumableSystem;

        private void Awake()
        {
            _equipmentSystem = new EquipmentSystem(equipInventory, baseInventory);
            _consumableSystem = new ConsumableSystem(baseInventory);
        }

        [Button]
        private void AddItem(InventoryItemConfig itemConfig)
        {
            baseInventory.AddItem(itemConfig.Clone);
        }
        
        [Button]
        private void ConsumeItem(InventoryItemConfig itemConfig)
        {
            _consumableSystem.ConsumeItem(itemConfig.Clone);
        }
        
        [Button]
        private void EquipItem(InventoryItemConfig itemConfig)
        {
            _equipmentSystem.EquipItem(itemConfig.Clone);
        }
        
        [Button]
        private void UnEquipItem(EquipmentSlot unEquipSlot)
        {
            _equipmentSystem.UnEquipItem(unEquipSlot);
        }
        
        [Button]
        private void RemoveItem(InventoryItemConfig itemConfig, bool removeAllStack)
        {
            baseInventory.RemoveItem(itemConfig.Clone, removeAllStack);
        }
    }
}