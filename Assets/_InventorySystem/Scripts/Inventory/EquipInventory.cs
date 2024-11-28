// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-28
// <file>: EquipInventory.cs
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using _InventorySystem.Scripts.Extensions;
using _InventorySystem.Scripts.Item;
using _InventorySystem.Scripts.Item.Components;
using Sirenix.OdinInspector;

namespace _InventorySystem.Scripts.Inventory
{
    [Serializable]
    public sealed class EquipInventory
    {
        [ReadOnly, ShowInInspector] private readonly Dictionary<EquipmentSlot, InventoryItem> _equipmentSlots;

        public EquipInventory()
        {
            _equipmentSlots = new Dictionary<EquipmentSlot, InventoryItem>();
        }

        public bool EquipItem(InventoryItem equipItem, out InventoryItem oldEquipItem)
        {
            oldEquipItem = default;
            if (!CanEquip(equipItem, out var componentEquippable))
            {
                return false;
            }
            
            if (_equipmentSlots.ContainsKey(componentEquippable.EquipmentSlot))
            {
                oldEquipItem = _equipmentSlots[componentEquippable.EquipmentSlot];
                _equipmentSlots[componentEquippable.EquipmentSlot] = equipItem;
                return true;
            }
            
            _equipmentSlots[componentEquippable.EquipmentSlot] = equipItem;
            return true;
        }

        private bool CanEquip(InventoryItem equipItem, out IInventoryItemComponentEquippable componentEquippable)
        {
            return equipItem.TryGetComponentSafe(InventoryItemFlags.EQUIPPABLE, out componentEquippable);
        }
        
        public bool TryUnEquipItem(EquipmentSlot unEquipSlot, out InventoryItem unEquipItem)
        {
            if (_equipmentSlots.TryGetValue(unEquipSlot, out var equipItem))
            {
                _equipmentSlots.Remove(unEquipSlot);
                unEquipItem = equipItem;
                return true;
            }

            unEquipItem = default;
            return false;
        }
    }
}