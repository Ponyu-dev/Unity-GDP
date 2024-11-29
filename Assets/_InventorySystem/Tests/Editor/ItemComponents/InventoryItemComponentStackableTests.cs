// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-28
// <file>: InventoryItemComponentStackableTests.cs
// ------------------------------------------------------------------------------

using _InventorySystem.Scripts.Item.Components;
using NUnit.Framework;

namespace _InventorySystem.Tests.Editor.ItemComponents
{
    [TestFixture]
    public sealed class InventoryItemComponentStackableTests
    {
        private InventoryItemComponentStackable item;

        [SetUp]
        public void SetUp()
        {
            item = new InventoryItemComponentStackable(0);
        }

        [Test]
        public void Given_NewItem_When_Initialized_Then_CountShouldBeZero()
        {
            Assert.AreEqual(0, item.Count);
        }

        [Test]
        public void Given_ItemWithCountZero_When_IncrementCalledWithPositiveStep_Then_CountShouldIncrease()
        {
            item.Increment(5);
            Assert.AreEqual(5, item.Count);
        }

        [Test]
        public void Given_ItemWithCountZero_When_IncrementCalledWithNegativeStep_Then_CountShouldNotChange()
        {
            item.Increment(-3);
            Assert.AreEqual(0, item.Count);
        }

        [Test]
        public void Given_ItemWithCountFive_When_DecrementCalledWithStep_Then_CountShouldDecrease()
        {
            item.Increment(5);
            item.Decrement(2);
            Assert.AreEqual(3, item.Count);
        }

        [Test]
        public void Given_ItemWithCountZero_When_DecrementCalled_Then_CountShouldNotGoBelowZero()
        {
            item.Decrement(3);
            Assert.AreEqual(0, item.Count);
        }

        [Test]
        public void Given_ItemWithCountFive_When_DecrementCalledWithStepGreaterThanCount_Then_CountShouldBeZero()
        {
            item.Increment(5);
            item.Decrement(6);
            Assert.AreEqual(0, item.Count);
        }
        
        [Test]
        public void Given_ItemWithCountFive_When_DecrementCalledWithNegativeStep_Then_CountShouldNotChange()
        {
            item.Increment(5);
            item.Decrement(-2);
            Assert.AreEqual(5, item.Count);
        }

        [Test]
        public void Given_ItemWithCountOne_When_IsNotEmptyCalled_Then_ShouldReturnTrue()
        {
            item.Increment(1);
            Assert.IsTrue(item.IsNotEmpty());
        }

        [Test]
        public void Given_ItemWithCountZero_When_IsNotEmptyCalled_Then_ShouldReturnFalse()
        {
            Assert.IsFalse(item.IsNotEmpty());
        }

        [Test]
        public void Given_ItemWithCountFive_When_Cloned_Then_ClonedItemShouldHaveSameCountAndBeDifferentInstance()
        {
            item.Increment(5);
            var clonedItem = item.Clone() as InventoryItemComponentStackable;

            Assert.IsNotNull(clonedItem);
            Assert.AreEqual(item.Count, clonedItem.Count);
            Assert.AreNotSame(item, clonedItem);
        }
    }
}