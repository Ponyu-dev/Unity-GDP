// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-30
// <file>: InventoryItemComponentEffectibleHealingHitPoint.cs
// ------------------------------------------------------------------------------

using System;
using _InventorySystem.HeroStatsDebug.Scripts;
using UnityEngine;

namespace _InventorySystem.Scripts.Item.Components
{
    [Serializable]
    public sealed class InventoryItemComponentEffectibleHealingHitPoint : InventoryItemComponentEffectible
    {
        [SerializeField] private int amountHill;
        public int AmountHill => amountHill;

        public override void EffectActive(HeroStats heroStats)
        {
            heroStats.HitPointsComponent.UpdateCurrentHp(amountHill);
        }

        public override IInventoryItemComponent Clone()
        {
            return new InventoryItemComponentEffectibleHealingHitPoint
            {
                amountHill = AmountHill
            };
        }
    }
}