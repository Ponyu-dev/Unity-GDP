// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-28
// <file>: InventoryItemComponentConsumable.cs
// ------------------------------------------------------------------------------

using System;
using UnityEngine;

namespace _InventorySystem.Scripts.Item.Components
{
    public interface IInventoryItemComponentConsumable : IInventoryItemComponent
    {
        int ConsumeAmount { get; }
        int IncreaseAmount { get; }
    }
    
    [Serializable]
    public sealed class InventoryItemComponentConsumable : IInventoryItemComponentConsumable
    {
        [SerializeField] private int consumeAmount;
        public int ConsumeAmount => consumeAmount;
        
        [SerializeField] private int increaseAmount;
        public int IncreaseAmount => increaseAmount;

        public IInventoryItemComponent Clone()
        {
            return new InventoryItemComponentConsumable
            {
                consumeAmount = ConsumeAmount,
                increaseAmount = IncreaseAmount
            };
        }
    }
}