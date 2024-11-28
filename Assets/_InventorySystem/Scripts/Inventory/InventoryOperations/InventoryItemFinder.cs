// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-28
// <file>: InventoryItemFinder.cs
// ------------------------------------------------------------------------------

using System;
using _InventorySystem.Scripts.Item;

namespace _InventorySystem.Scripts.Inventory.InventoryOperations
{
    /*public sealed class InventoryItemFinder
    {
        private readonly ListInventory _listInventory;
        
        public InventoryItemFinder(ListInventory listInventory)
        {
            _listInventory = listInventory;
        }

        public bool TryFindItemIndex(InventoryItem findItem, out int foundItemIndex)
        {
            for (int i = 0, count = _listInventory.Items.Count; i < count; i++)
            {
                var item = _listInventory.Items[i];
                
                if (!IsEqualItems(item, findItem))
                    continue;
                
                foundItemIndex = i;
                return true;
            }

            foundItemIndex = -1;
            return false;
        }
        
        public bool TryFindItem(InventoryItem findItem, out InventoryItem foundItem)
        {
            for (int i = 0, count = _listInventory.Items.Count; i < count; i++)
            {
                var item = _listInventory.Items[i];
                
                if (!IsEqualItems(item, findItem))
                    continue;
                
                foundItem = item;
                return true;
            }

            foundItem = default;
            return false;
        }

        private bool IsEqualItems(InventoryItem item1, InventoryItem item2)
        {
            if (item1 is null && item2 is null)
                return true;
            
            if (item1 is null || item2 is null)
                return false;
            
            return string.Equals(item1.Id, item2.Id, StringComparison.Ordinal);
        }
    }*/
}