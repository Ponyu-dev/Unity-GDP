// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-29
// <file>: EquipmentSystem.cs
// ------------------------------------------------------------------------------

using System;
using _InventorySystem.Scripts.Item;
using _InventorySystem.Scripts.Item.Components;
using VContainer;

namespace _InventorySystem.Scripts.Inventory.System
{
    public interface IEquipmentSystemAction
    {
        event Action<InventoryItem> OnEquipItem;
        event Action<InventoryItem> OnUnEquipItem;
    }
    
    public interface IEquipmentSystem
    {
        void EquipItem(InventoryItem inventoryItem);
        void UnEquipItem(EquipmentSlot unEquipSlot);
    }
    
    public sealed class EquipmentSystem : IEquipmentSystem, IEquipmentSystemAction
    {
        public event Action<InventoryItem> OnEquipItem;
        public event Action<InventoryItem> OnUnEquipItem;
        
        private readonly IEquipInventory _equipInventory;
        private readonly IBaseInventory _baseInventory;

        [Inject]
        public EquipmentSystem(IEquipInventory equipInventory, IBaseInventory baseInventory)
        {
            _equipInventory = equipInventory;
            _baseInventory = baseInventory;
        }
        
        public void EquipItem(InventoryItem inventoryItem)
        {
            if (!_equipInventory.EquipItem(inventoryItem, out var oldEquipItem))
                return;
            
            OnEquipItem?.Invoke(inventoryItem);
            OnUnEquipItem?.Invoke(oldEquipItem);
            
            _baseInventory.RemoveItem(inventoryItem, true);
            
            if (oldEquipItem != null)
                _baseInventory.AddItem(oldEquipItem);
        }

        public void UnEquipItem(EquipmentSlot unEquipSlot)
        {
            if (!_equipInventory.TryUnEquipItem(unEquipSlot, out var unEquipItem))
                return;
            
            OnUnEquipItem?.Invoke(unEquipItem);
            _baseInventory.AddItem(unEquipItem);
        }
    }
}