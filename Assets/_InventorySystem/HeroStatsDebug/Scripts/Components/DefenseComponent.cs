// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-12-01
// <file>: DefenseComponent.cs
// ------------------------------------------------------------------------------

using System;
using UnityEngine;

namespace _InventorySystem.HeroStatsDebug.Scripts.Components
{
    [Serializable]
    public sealed class DefenseComponent : IStatComponent
    {
        [SerializeField] private int defense;

        public DefenseComponent(int initialDefense)
        {
            defense = Mathf.Max(initialDefense, 0);
        }

        public void UpdateValue(int value)
        {
            defense = Mathf.Max(defense + value, 0);
        }

        public int GetValue() => defense;
    }
}