// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-29
// <file>: BaseInventoryAddItemTests.cs
// ------------------------------------------------------------------------------

using System.Linq;
using _InventorySystem.Scripts.Inventory;
using _InventorySystem.Tests.Editor.Helpers;
using NUnit.Framework;

namespace _InventorySystem.Tests.Editor.Inventory
{
    [TestFixture]
    public sealed class BaseInventoryAddItemTests
    {
        private IBaseInventory _inventory;
        private HelpersForTests _helpersForTests;

        [SetUp]
        public void SetUp()
        {
            _inventory = new Scripts.Inventory.BaseInventory();
            _helpersForTests = new HelpersForTests();
        }

        [Test]
        public void Given_InventoryIsEmpty_When_AddingNonStackableItem_Then_ItemIsAddedAndEventFires()
        {
            var eventFired = false;
            _inventory.OnItemAdded += _ => eventFired = true;
            _inventory.AddItem(_helpersForTests.ItemNone);
            Assert.AreEqual(1, _inventory.Items.Count);
            Assert.IsTrue(eventFired);
        }


        [Test]
        public void Given_InventoryIsEmpty_When_AddingNullItem_Then_NoItemIsAddedAndNoEventFires()
        {
            var eventFired = false;
            _inventory.OnItemAdded += _ => eventFired = true;

            _inventory.AddItem(null);

            Assert.IsFalse(eventFired);
            Assert.AreEqual(0, _inventory.Items.Count);
        }

        [Test]
        public void Given_InventoryIsEmpty_When_AddingDefaultItem_Then_NoItemIsAddedAndNoEventFires()
        {
            var eventFired = false;
            _inventory.OnItemAdded += _ => eventFired = true;

            _inventory.AddItem(default);

            Assert.IsFalse(eventFired);
            Assert.AreEqual(0, _inventory.Items.Count);
        }

        [Test]
        public void Given_InventoryHasSameStackableItem_When_AddingStackableItem_Then_ItemStackIncreasesAndEventFires()
        {
            var eventFired = false;
            _inventory.OnItemStackChanged += _ => eventFired = true;

            _inventory.AddItem(_helpersForTests.ItemStackable);
            _inventory.AddItem(_helpersForTests.ItemStackable);

            Assert.IsTrue(eventFired);
            Assert.AreEqual(1, _inventory.Items.Count);
        }

        [Test]
        public void Given_InventoryHasDifferentItems_When_AddingNewStackableItem_Then_ItemIsAddedAndEventFires()
        {
            var eventItemAddedFired = false;
            _inventory.OnItemAdded += _ => eventItemAddedFired = true;

            var eventItemStackChanged = false;
            _inventory.OnItemStackChanged += _ => eventItemStackChanged = true;

            _inventory.AddItem(_helpersForTests.ItemStackable);
            _inventory.AddItem(_helpersForTests.ItemEquippableBody);

            Assert.IsTrue(eventItemAddedFired);
            Assert.IsFalse(eventItemStackChanged);
            Assert.AreEqual(2, _inventory.Items.Count);
            Assert.AreEqual(_helpersForTests.ItemStackable, _inventory.Items[0]);
        }

        [Test]
        public void
            Given_StackableAndNonStackableItem_When_AddedToInventory_Then_StackableItemIsStackedAndNonStackableIsNot()
        {
            var eventItemAddedFired = 0;
            _inventory.OnItemAdded += _ => eventItemAddedFired += 1;

            var eventItemStackChanged = 0;
            _inventory.OnItemStackChanged += _ => eventItemStackChanged += 1;

            _inventory.AddItem(_helpersForTests.ItemStackable);
            _inventory.AddItem(_helpersForTests.ItemStackableNotComponent);

            Assert.AreEqual(2, eventItemAddedFired);
            Assert.AreEqual(0, eventItemStackChanged);
            Assert.AreEqual(2, _inventory.Items.Count);
            Assert.AreEqual(_helpersForTests.ItemStackableNotComponent, _inventory.Items[1]);
        }

        [Test]
        public void Given_InventoryHasItem_When_RemovingNonStackableItem_Then_ItemIsRemovedAndEventFires()
        {
            var eventFired = false;
            _inventory.OnItemRemoved += _ => eventFired = true;
            
            _inventory.AddItem(_helpersForTests.ItemNone);
            _inventory.RemoveItem(_helpersForTests.ItemNone);

            Assert.IsTrue(eventFired);
            Assert.AreEqual(0, _inventory.Items.Count);
        }

        [Test]
        public void Given_InventoryHasStackableItem_When_RemovingStackableItemWithoutRemoveAllStack_Then_StackDecreasesAndEventFires()
        {
            var eventItemRemovedFired = false;
            _inventory.OnItemRemoved += _ => eventItemRemovedFired = true;
            var eventItemStackChangedFired = false;
            _inventory.OnItemStackChanged += _ => eventItemStackChangedFired = true;
            
            _inventory.AddItem(_helpersForTests.ItemStackable);
            _inventory.AddItem(_helpersForTests.ItemStackable);

            _inventory.RemoveItem(_helpersForTests.ItemStackable);

            Assert.IsTrue(eventItemStackChangedFired);
            Assert.IsFalse(eventItemRemovedFired);
            Assert.AreEqual(1, _inventory.Items.Count);
        }

        [Test]
        public void Given_InventoryHasStackableItem_When_RemovingStackableItemWithRemoveAllStack_Then_ItemIsRemovedAndEventFires()
        {
            var eventFired = false;
            _inventory.OnItemRemoved += _ => eventFired = true;
            
            _inventory.AddItem(_helpersForTests.ItemStackable);
            _inventory.AddItem(_helpersForTests.ItemStackable);

            _inventory.RemoveItem(_helpersForTests.ItemStackable, true);

            Assert.IsTrue(eventFired);
            Assert.AreEqual(0, _inventory.Items.Count);
        }
        
        [Test]
        public void Given_InventoryDoesNotHaveItem_When_RemovingAnyItem_Then_NoActionIsTakenAndNoEventFires()
        {
            var eventFired = false;
            _inventory.OnItemRemoved += _ => eventFired = true;

            _inventory.RemoveItem(_helpersForTests.ItemNone);

            Assert.IsFalse(eventFired);
            Assert.AreEqual(0, _inventory.Items.Count);
        }

        [Test]
        public void Given_InventoryHasMultipleItems_When_RemovingSpecificItem_Then_OnlyThatItemIsRemoved()
        {
            var item1 = _helpersForTests.ItemNone;
            var item2 = _helpersForTests.ItemNone;
            _inventory.AddItem(item1);
            _inventory.AddItem(item2);
            _inventory.RemoveItem(item1);
            Assert.AreEqual(1, _inventory.Items.Count);
            Assert.IsTrue(_inventory.Items.Contains(item2));
        }
    }
}