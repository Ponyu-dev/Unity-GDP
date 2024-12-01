// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-12-01
// <file>: SpeedComponent.cs
// ------------------------------------------------------------------------------

using System;
using UnityEngine;

namespace _InventorySystem.HeroStatsDebug.Scripts.Components
{
    [Serializable]
    public sealed class SpeedComponent : IStatComponent
    {
        [SerializeField] private int speed;

        public SpeedComponent(int initialSpeed)
        {
            speed = Mathf.Max(initialSpeed, 0);
        }

        public void UpdateValue(int value)
        {
            speed = Mathf.Max(speed + value, 0);
        }

        public int GetValue() => speed;
    }
}