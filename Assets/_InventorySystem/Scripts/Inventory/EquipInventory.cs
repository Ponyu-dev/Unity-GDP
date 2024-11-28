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
using UnityEngine;

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
                Log("Smth");
                return false;
            }
            
            if (_equipmentSlots.ContainsKey(componentEquippable.EquipmentSlot))
            {
                oldEquipItem = _equipmentSlots[componentEquippable.EquipmentSlot];
                _equipmentSlots[componentEquippable.EquipmentSlot] = equipItem;
                Log($"Заменили {oldEquipItem} на {equipItem} в слоте {componentEquippable.EquipmentSlot}");
                return true;
            }
            
            _equipmentSlots[componentEquippable.EquipmentSlot] = equipItem;
            Log($"Экипирован {equipItem} в слот {componentEquippable.EquipmentSlot}");
            return true;
        }

        private bool CanEquip(InventoryItem equipItem, out IInventoryItemComponentEquippable componentEquippable)
        {
            componentEquippable = default;
            
            if (!equipItem.FlagsExists(InventoryItemFlags.EQUIPPABLE))
            {
                return false;
            }

            if (!equipItem.TryGetComponent(out componentEquippable))
            {
                return false;
            }

            return true;
        }
        
        public bool TryUnEquipItem(EquipmentSlot unEquipSlot, out InventoryItem unEquipItem)
        {
            if (_equipmentSlots.TryGetValue(unEquipSlot, out var equipItem))
            {
                _equipmentSlots.Remove(unEquipSlot);
                unEquipItem = equipItem;
                Log($"Снято {equipItem} с слота {unEquipSlot}");
                return true;
            }

            unEquipItem = default;
            return false;
        }

        private void Log(string message)
        {
            Debug.Log(message);
        }
    }
}