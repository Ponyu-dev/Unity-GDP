// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-28
// <file>: InventoryItemAdder.cs
// ------------------------------------------------------------------------------

using _InventorySystem.Scripts.Item;

namespace _InventorySystem.Scripts.Inventory.InventoryOperations
{
    public sealed class InventoryItemAdder
    {
        private readonly ListInventory _listInventory;
        
        public InventoryItemAdder(ListInventory listInventory)
        {
            _listInventory = listInventory;
        }

        public void Add(InventoryItem foundItem)
        {
            _listInventory.Add(foundItem);
        }
    }
}