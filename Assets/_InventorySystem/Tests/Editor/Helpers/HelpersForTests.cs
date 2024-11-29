// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-29
// <file>: HelpersForTests.cs
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using _InventorySystem.Scripts.Item;
using _InventorySystem.Scripts.Item.Components;

namespace _InventorySystem.Tests.Editor.Helpers
{
    public class HelpersForTests
    {
        public InventoryItem ItemNone = new InventoryItem();
        
        public InventoryItem ItemStackable = new InventoryItem(
            "STACKABLE",
            Guid.NewGuid().ToString(),
            InventoryItemFlags.STACKABLE,
            new InventoryItemMetadata { title = "STACKABLE", decription = "STACKABLE", icon = default },
            new List<IInventoryItemComponent>
            {
                new InventoryItemComponentStackable(1)
            });
        
        public InventoryItem ItemStackableNotComponent = new InventoryItem(
            "STACKABLE",
            Guid.NewGuid().ToString(),
            InventoryItemFlags.STACKABLE,
            new InventoryItemMetadata { title = "STACKABLE", decription = "STACKABLE", icon = default },
            new List<IInventoryItemComponent>());
        
        public InventoryItem ItemEquippableBody = new InventoryItem(
            "EQUIPPABLE-Body",
            Guid.NewGuid().ToString(),
            InventoryItemFlags.EQUIPPABLE,
            new InventoryItemMetadata { title = "EQUIPPABLE-Body", decription = "EQUIPPABLE-Body", icon = default },
            new List<IInventoryItemComponent>
            {
                new InventoryItemComponentEquippable(EquipmentSlot.Body)
            });
        
        public InventoryItem ItemEquippableLegs = new InventoryItem(
            "EQUIPPABLE-Legs",
            Guid.NewGuid().ToString(),
            InventoryItemFlags.EQUIPPABLE,
            new InventoryItemMetadata { title = "EQUIPPABLE-Legs", decription = "EQUIPPABLE-Legs", icon = default },
            new List<IInventoryItemComponent>
            {
                new InventoryItemComponentEquippable(EquipmentSlot.Legs)
            });
        
        public InventoryItem ItemEquippableHead = new InventoryItem(
            "EQUIPPABLE-Head",
            Guid.NewGuid().ToString(),
            InventoryItemFlags.EQUIPPABLE,
            new InventoryItemMetadata { title = "EQUIPPABLE-Head", decription = "EQUIPPABLE-Head", icon = default },
            new List<IInventoryItemComponent>
            {
                new InventoryItemComponentEquippable(EquipmentSlot.Head)
            });
        
        public InventoryItem ItemEquippableLeftHand = new InventoryItem(
            "EQUIPPABLE-LeftHand",
            Guid.NewGuid().ToString(),
            InventoryItemFlags.EQUIPPABLE,
            new InventoryItemMetadata { title = "EQUIPPABLE-LeftHand", decription = "EQUIPPABLE-LeftHand", icon = default },
            new List<IInventoryItemComponent>
            {
                new InventoryItemComponentEquippable(EquipmentSlot.LeftHand)
            });
        
        public InventoryItem ItemEquippableRightHand = new InventoryItem(
            "EQUIPPABLE-RightHand",
            Guid.NewGuid().ToString(),
            InventoryItemFlags.EQUIPPABLE,
            new InventoryItemMetadata { title = "EQUIPPABLE-RightHand", decription = "EQUIPPABLE-RightHand", icon = default },
            new List<IInventoryItemComponent>
            {
                new InventoryItemComponentEquippable(EquipmentSlot.RightHand)
            });
        
        public InventoryItem ItemConsume = new InventoryItem(
            "CONSUMABLE",
            Guid.NewGuid().ToString(),
            InventoryItemFlags.CONSUMABLE,
            new InventoryItemMetadata { title = "CONSUMABLE", decription = "CONSUMABLE", icon = default },
            new List<IInventoryItemComponent>
            {
                new InventoryItemComponentConsumable()
            });
        
        public InventoryItem ItemStackableConsume = new InventoryItem(
            "STACKABLE/CONSUMABLE",
            Guid.NewGuid().ToString(),
            InventoryItemFlags.STACKABLE & InventoryItemFlags.CONSUMABLE,
            new InventoryItemMetadata { title = "STACKABLE/CONSUMABLE", decription = "STACKABLE/CONSUMABLE", icon = default },
            new List<IInventoryItemComponent>
            {
                new InventoryItemComponentStackable(0),
                new InventoryItemComponentConsumable()
            });
    }
}