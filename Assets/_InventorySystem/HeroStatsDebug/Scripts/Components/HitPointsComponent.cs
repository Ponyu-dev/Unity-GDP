// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-11-30
// <file>: HitPointsComponent.cs
// ------------------------------------------------------------------------------

using System;
using UnityEngine;

namespace _InventorySystem.HeroStatsDebug.Scripts.Components
{
    public interface IHitPointsComponent
    {
        int CurrentHitPoints { get; }
        int MaxHitPoints { get; }
        void UpdateCurrentHp(int value);
        void UpdateMaxHp(int value);
    }
    
    [Serializable]
    public sealed class HitPointsComponent : IHitPointsComponent
    {
        [SerializeField] private int hitPoints;
        public int CurrentHitPoints => hitPoints;
        
        [SerializeField] private int maxHitPoints;
        public int MaxHitPoints => maxHitPoints;

        public HitPointsComponent(int hitPoints, int maxHitPoints)
        {
            this.maxHitPoints = Mathf.Max(maxHitPoints, 0);
            this.hitPoints = Mathf.Clamp(hitPoints, 0, this.maxHitPoints);
        }
        
        public void UpdateMaxHp(int value)
        {
            var newMaxHp = maxHitPoints + value;
            maxHitPoints = Mathf.Max(newMaxHp, 0);
            
            hitPoints = Mathf.Clamp(hitPoints, 0, maxHitPoints);
        }

        public void UpdateCurrentHp(int value)
        {
            hitPoints = Mathf.Clamp(hitPoints + value, 0, maxHitPoints);
        }
    }
}