// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-12-01
// <file>: InventoryItemComponentEffectibleUpdateMaxHitPoint.cs
// ------------------------------------------------------------------------------

using System;
using _InventorySystem.HeroStatsDebug.Scripts;
using UnityEngine;

namespace _InventorySystem.Scripts.Item.Components
{
    [Serializable]
    public sealed class InventoryItemComponentEffectibleUpdateMaxHitPoint : InventoryItemComponentEffectible
    {
        [SerializeField] private int stepMaxHitPoints;
        public int StepMaxHitPoints => stepMaxHitPoints;

        public override void EffectActive(HeroStats heroStats)
        {
            heroStats.HitPointsComponent.UpdateMaxHp(stepMaxHitPoints);
        }

        public override void EffectUnActive(HeroStats heroStats)
        {
            heroStats.HitPointsComponent.UpdateMaxHp(-stepMaxHitPoints);
        }

        public override IInventoryItemComponent Clone()
        {
            return new InventoryItemComponentEffectibleUpdateMaxHitPoint
            {
                stepMaxHitPoints = StepMaxHitPoints
            };
        }
    }
}