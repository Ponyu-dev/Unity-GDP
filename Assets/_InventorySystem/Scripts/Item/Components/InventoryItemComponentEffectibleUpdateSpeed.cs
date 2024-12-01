// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-12-01
// <file>: InventoryItemComponentEffectibleUpdateSpeed.cs
// ------------------------------------------------------------------------------

using System;
using _InventorySystem.HeroStatsDebug.Scripts;
using UnityEngine;

namespace _InventorySystem.Scripts.Item.Components
{
    [Serializable]
    public sealed class InventoryItemComponentEffectibleUpdateSpeed : InventoryItemComponentEffectible
    {
        [SerializeField] private int amountSpeed;
        public int AmountSpeed => amountSpeed;

        public override void EffectActive(HeroStats heroStats)
        {
            heroStats.SpeedComponent.UpdateValue(amountSpeed);
        }
        
        public override void EffectUnActive(HeroStats heroStats)
        {
            heroStats.SpeedComponent.UpdateValue(-amountSpeed);
        }

        public override IInventoryItemComponent Clone()
        {
            return new InventoryItemComponentEffectibleUpdateSpeed
            {
                amountSpeed = AmountSpeed
            };
        }
    }
}