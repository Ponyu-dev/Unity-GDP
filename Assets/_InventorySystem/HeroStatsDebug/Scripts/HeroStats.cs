// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-30
// <file>: HeroStats.cs
// ------------------------------------------------------------------------------

using System;
using _InventorySystem.HeroStatsDebug.Scripts.Components;
using UnityEngine;

namespace _InventorySystem.HeroStatsDebug.Scripts
{
    [Serializable]
    public sealed class HeroStats
    {
        [SerializeField] private HitPointsComponent hitPointsComponent;
        public IHitPointsComponent HitPointsComponent => hitPointsComponent;
        
        [SerializeField] private SpeedComponent speedComponent;
        public IStatComponent SpeedComponent => speedComponent;
        
        [SerializeField] private DefenseComponent defenseComponent;
        public IStatComponent DefenseComponent => defenseComponent;
        
        [SerializeField] private AttackComponent attackComponent;
        public IStatComponent AttackComponent => attackComponent;
    }
}