// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-12-01
// <file>: InventoryItemComponentEffectibleUpdateAttack.cs
// ------------------------------------------------------------------------------

using System;
using _InventorySystem.HeroStatsDebug.Scripts;
using UnityEngine;

namespace _InventorySystem.Scripts.Item.Components
{
    [Serializable]
    public sealed class InventoryItemComponentEffectibleUpdateAttack : InventoryItemComponentEffectible
    {
        [SerializeField] private int amountDamage;
        public int AmountDamage => amountDamage;

        public override void EffectActive(HeroStats heroStats)
        {
            heroStats.AttackComponent.UpdateValue(amountDamage);
        }
        
        public override void EffectUnActive(HeroStats heroStats)
        {
            heroStats.AttackComponent.UpdateValue(-amountDamage);
        }

        public override IInventoryItemComponent Clone()
        {
            return new InventoryItemComponentEffectibleUpdateAttack
            {
                amountDamage = AmountDamage
            };
        }
    }
}