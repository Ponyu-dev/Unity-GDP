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
using VContainer;

namespace _InventorySystem.Scripts
{
    public class BaseInventoryDebug : MonoBehaviour
    {
        [ShowInInspector, Inject] private BaseInventory baseInventory;
        [ShowInInspector, Inject] private EquipInventory equipInventory;
        
        [Inject] private EquipmentSystem _equipmentSystem;
        [Inject] private ConsumableSystem _consumableSystem;

        /*private void Awake()
        {
            _equipmentSystem = new EquipmentSystem(equipInventory, baseInventory);
            _consumableSystem = new ConsumableSystem(baseInventory);
        }*/

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