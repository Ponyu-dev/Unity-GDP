// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-29
// <file>: EquipmentSystem.cs
// ------------------------------------------------------------------------------

using _InventorySystem.Scripts.Item;
using _InventorySystem.Scripts.Item.Components;

namespace _InventorySystem.Scripts.Inventory.System
{
    public interface IEquipmentSystem
    {
        void EquipItem(InventoryItem inventoryItem);
        void UnEquipItem(EquipmentSlot unEquipSlot);
    }
    
    public sealed class EquipmentSystem : IEquipmentSystem
    {
        private readonly IEquipInventory _equipInventory;
        private readonly IBaseInventory _baseInventory;

        public EquipmentSystem(IEquipInventory equipInventory, IBaseInventory baseInventory)
        {
            _equipInventory = equipInventory;
            _baseInventory = baseInventory;
        }
        
        public void EquipItem(InventoryItem inventoryItem)
        {
            if (!_equipInventory.EquipItem(inventoryItem, out var oldEquipItem))
                return;
            
            _baseInventory.RemoveItem(inventoryItem, true);
            
            if (oldEquipItem != null)
                _baseInventory.AddItem(oldEquipItem);
        }

        public void UnEquipItem(EquipmentSlot unEquipSlot)
        {
            if (_equipInventory.TryUnEquipItem(unEquipSlot, out var unEquipItem))
                _baseInventory.AddItem(unEquipItem);
        }
    }
}