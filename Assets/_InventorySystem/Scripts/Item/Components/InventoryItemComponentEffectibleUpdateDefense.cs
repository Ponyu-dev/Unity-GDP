// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-12-01
// <file>: InventoryItemComponentEffectibleUpdateDefense.cs
// ------------------------------------------------------------------------------

using System;
using _InventorySystem.HeroStatsDebug.Scripts;
using UnityEngine;

namespace _InventorySystem.Scripts.Item.Components
{
    [Serializable]
    public sealed class InventoryItemComponentEffectibleUpdateDefense : InventoryItemComponentEffectible
    {
        [SerializeField] private int amountDefense;
        public int AmountDefense => amountDefense;

        public override void EffectActive(HeroStats heroStats)
        {
            heroStats.DefenseComponent.UpdateValue(amountDefense);
        }
        
        public override void EffectUnActive(HeroStats heroStats)
        {
            heroStats.DefenseComponent.UpdateValue(-amountDefense);
        }

        public override IInventoryItemComponent Clone()
        {
            return new InventoryItemComponentEffectibleUpdateDefense
            {
                amountDefense = AmountDefense
            };
        }
    }
}