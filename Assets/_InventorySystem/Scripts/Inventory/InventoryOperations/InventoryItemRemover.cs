// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-28
// <file>: InventoryItemRemover.cs
// ------------------------------------------------------------------------------

using _InventorySystem.Scripts.Item;

namespace _InventorySystem.Scripts.Inventory.InventoryOperations
{
    public sealed class InventoryItemRemover
    {
        private readonly ListInventory _listInventory;
        
        public InventoryItemRemover(ListInventory listInventory)
        {
            _listInventory = listInventory;
        }

        public void Remove(InventoryItem item)
        {
            _listInventory.Remove(item);
        }
    }
}