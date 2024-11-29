// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-29
// <file>: EquipInventoryTests.cs
// ------------------------------------------------------------------------------

using _InventorySystem.Scripts.Inventory;
using _InventorySystem.Scripts.Item.Components;
using _InventorySystem.Tests.Editor.Helpers;
using NUnit.Framework;

namespace _InventorySystem.Tests.Editor.Inventory
{
    [TestFixture]
    public class EquipInventoryTests
    {
        private EquipInventory _equipInventory;
        private HelpersForTests _helpersForTests;

        [SetUp]
        public void SetUp()
        {
            _equipInventory = new EquipInventory();
            _helpersForTests = new HelpersForTests();
        }

        [Test]
        public void Given_EquippedItem_When_EquippingNewItem_Then_TheOldItemIsReturned()
        {
            // Arrange
            var equipItem1 = _helpersForTests.ItemEquippableBody;
            var equipItem2 = _helpersForTests.ItemEquippableBody;

            // Simulate equipping an item in the 'Body' slot first
            _equipInventory.EquipItem(equipItem1, out _);

            // Act
            var result = _equipInventory.EquipItem(equipItem2, out var oldEquipItem);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(equipItem1, oldEquipItem);
        }

        [Test]
        public void Given_UnEquippedSlot_When_UnEquippingItem_Then_ItemIsRemovedFromInventory()
        {
            // Arrange
            var equipItemBody = _helpersForTests.ItemEquippableBody;
            var equipmentSlotBody = EquipmentSlot.Body;

            // Equip an item first
            _equipInventory.EquipItem(equipItemBody, out _);

            // Act
            bool result = _equipInventory.TryUnEquipItem(equipmentSlotBody, out var unEquipItem);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(equipItemBody, unEquipItem);
            Assert.AreEqual(0, _equipInventory.EquipmentSlots.Count);
        }

        [Test]
        public void Given_UnEquippedSlot_When_UnEquippingItem_Then_ReturnsFalseAndNullItem()
        {
            // Arrange
            var equipmentSlot = EquipmentSlot.Body;

            // Act
            var result = _equipInventory.TryUnEquipItem(equipmentSlot, out var unEquipItem);

            // Assert
            Assert.IsFalse(result);
            Assert.IsNull(unEquipItem);
        }

        [Test]
        public void Given_EquippedItem_When_EquippingItemInDifferentSlot_Then_ItemIsEquippedInNewSlot()
        {
            // Arrange
            var itemBody = _helpersForTests.ItemEquippableBody;
            var slotBody = EquipmentSlot.Body;
            
            var itemHead = _helpersForTests.ItemEquippableHead;
            var slotHead = EquipmentSlot.Head;

            // Act
            var isEquipBody = _equipInventory.EquipItem(itemBody, out var oldEquipBody);
            var isEquipHead = _equipInventory.EquipItem(itemHead, out var oldEquipHead);

            // Assert
            Assert.IsTrue(isEquipBody);
            Assert.IsNull(oldEquipBody);
            Assert.AreEqual(itemBody, _equipInventory.EquipmentSlots[slotBody]);
            
            Assert.IsTrue(isEquipHead);
            Assert.IsNull(oldEquipHead);
            Assert.AreEqual(itemHead, _equipInventory.EquipmentSlots[slotHead]);
        }

        [Test]
        public void Given_ItemThatCannotBeEquipped_When_EquippingItem_Then_ReturnsFalse()
        {
            // Arrange
            var notEquipItem = _helpersForTests.ItemNone;

            // Act
            var result = _equipInventory.EquipItem(notEquipItem, out var oldEquipItem);

            // Assert
            Assert.IsFalse(result);
            Assert.IsNull(oldEquipItem);
        }
    }
}