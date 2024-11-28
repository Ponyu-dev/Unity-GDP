// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-28
// <file>: InventoryItemComponentEquippable.cs
// ------------------------------------------------------------------------------

using System;
using UnityEngine;

namespace _InventorySystem.Scripts.Item.Components
{
    public enum EquipmentSlot
    {
        Head,
        Body,
        Legs,
        LeftHand,
        RightHand
    }
    
    public interface IInventoryItemComponentEquippable : IInventoryItemComponent
    {
        EquipmentSlot EquipmentSlot { get; }
        int IncreaseAmount { get; }
    }
    
    [Serializable]
    public sealed class InventoryItemComponentEquippable : IInventoryItemComponentEquippable
    {
        [SerializeField] private EquipmentSlot equipmentSlot;
        public EquipmentSlot EquipmentSlot => equipmentSlot;
        
        [SerializeField] private int increaseAmount;
        public int IncreaseAmount => increaseAmount;

        public IInventoryItemComponent Clone()
        {
            return new InventoryItemComponentEquippable
            {
                equipmentSlot = EquipmentSlot,
                increaseAmount = IncreaseAmount
            };
        }
    }
}