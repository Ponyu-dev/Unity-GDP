// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-28
// <file>: EquipInventory.cs
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using _InventorySystem.Scripts.Item;
using _InventorySystem.Scripts.Item.Components;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace _InventorySystem.Scripts.Inventory
{
    public interface IEquipInventoryAction
    {
        event Action<InventoryItem> OnEquipChanged;
        event Action<EquipmentSlot> OnUnEquipChanged;
    }
    
    public interface IEquipInventory
    {
        bool EquipItem(InventoryItem equipItem, out InventoryItem oldEquipItem);
        bool TryUnEquipItem(EquipmentSlot unEquipSlot, out InventoryItem unEquipItem);
    }
    
    [Serializable]
    public sealed class EquipInventory : IEquipInventory, IEquipInventoryAction
    {
        [ReadOnly, ShowInInspector]
        private readonly Dictionary<EquipmentSlot, InventoryItem> _equipmentSlots;
        public IDictionary<EquipmentSlot, InventoryItem> EquipmentSlots => _equipmentSlots;
        
        public event Action<InventoryItem> OnEquipChanged;
        public event Action<EquipmentSlot> OnUnEquipChanged;
        
        [Inject]
        public EquipInventory()
        {
            _equipmentSlots = new Dictionary<EquipmentSlot, InventoryItem>();
        }

        public bool EquipItem(InventoryItem equipItem, out InventoryItem oldEquipItem)
        {
            oldEquipItem = default;
            if (!equipItem.TryGetComponentSafe<IInventoryItemComponentEquippable>(InventoryItemFlags.EQUIPPABLE, out var componentEquippable))
            {
                return false;
            }
            
            if (_equipmentSlots.ContainsKey(componentEquippable.EquipmentSlot))
            {
                oldEquipItem = _equipmentSlots[componentEquippable.EquipmentSlot];
                _equipmentSlots[componentEquippable.EquipmentSlot] = equipItem;
                OnEquipChanged?.Invoke(equipItem);
                return true;
            }
            
            _equipmentSlots[componentEquippable.EquipmentSlot] = equipItem;
            OnEquipChanged?.Invoke(equipItem);
            return true;
        }
        
        public bool TryUnEquipItem(EquipmentSlot unEquipSlot, out InventoryItem unEquipItem)
        {
            if (_equipmentSlots.TryGetValue(unEquipSlot, out var equipItem))
            {
                _equipmentSlots.Remove(unEquipSlot);
                unEquipItem = equipItem;
                OnUnEquipChanged?.Invoke(unEquipSlot);
                return true;
            }

            unEquipItem = default;
            return false;
        }
    }
}