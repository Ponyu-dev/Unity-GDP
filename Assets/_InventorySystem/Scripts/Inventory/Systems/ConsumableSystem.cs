// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-29
// <file>: ConsumableSystem.cs
// ------------------------------------------------------------------------------

using _InventorySystem.Scripts.Item;
using _InventorySystem.Scripts.Item.Components;

namespace _InventorySystem.Scripts.Inventory.System
{
    public interface IConsumableSystem
    {
        void ConsumeItem(InventoryItem inventoryItem);
    }
    
    public sealed class ConsumableSystem : IConsumableSystem
    {
        private readonly IBaseInventory _baseInventory;

        public ConsumableSystem(IBaseInventory baseInventory)
        {
            _baseInventory = baseInventory;
        }

        public void ConsumeItem(InventoryItem inventoryItem)
        {
            if (!inventoryItem.TryGetComponentSafe<IInventoryItemComponentConsumable>(InventoryItemFlags.CONSUMABLE, out var consumable))
                return;
            
            _baseInventory.RemoveItem(inventoryItem);
        }
    }
}