// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-28
// <file>: InventoryItemComponentStackable.cs
// ------------------------------------------------------------------------------

using System;
using UnityEngine;

namespace _InventorySystem.Scripts.Item.Components
{
    public interface IInventoryItemComponentStackable : IInventoryItemComponent
    {
        int Count { get; }
        void Increment(int step);
        void Decrement(int step);
        bool IsNotEmpty();
    }
    
    [Serializable]
    public sealed class InventoryItemComponentStackable : IInventoryItemComponentStackable
    {
        [SerializeField] private int count;
        public int Count => count;
        
        public void Increment(int step)
        {
            step = Math.Max(step, 0);
            count += step;
        }

        public void Decrement(int step)
        {
            step = Math.Clamp(step, 0, count);
            count -= step;
        }

        public bool IsNotEmpty() => Count > 0;
        
        public IInventoryItemComponent Clone()
        {
            return new InventoryItemComponentStackable
            {
                count = count
            };
        }
    }
}