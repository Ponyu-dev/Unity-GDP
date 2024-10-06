using System;
using UnityEngine;

namespace _EventBus.Scripts.Players.Hero
{
    [Serializable]
    public class HeroData
    {
        [SerializeField] public HeroType heroType;
        [SerializeField] public int damage;
        [SerializeField] public int health;
    }
}