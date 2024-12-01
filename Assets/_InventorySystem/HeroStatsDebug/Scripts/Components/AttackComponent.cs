// ------------------------------------------------------------------------------
// <author>: Iurii Ponomarev (Ponyu)
// <created>: 2024-12-01
// <file>: AttackComponent.cs
// ------------------------------------------------------------------------------

using System;
using UnityEngine;

namespace _InventorySystem.HeroStatsDebug.Scripts.Components
{
    [Serializable]
    public sealed class AttackComponent : IStatComponent
    {
        [SerializeField] private int attack;

        public AttackComponent(int initialAttack)
        {
            attack = Mathf.Max(initialAttack, 0);
        }

        public void UpdateValue(int value)
        {
            attack = Mathf.Max(attack + value, 0);
        }

        public int GetValue() => attack;
    }
}