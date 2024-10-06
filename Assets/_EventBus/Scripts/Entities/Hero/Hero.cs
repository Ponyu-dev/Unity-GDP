using System;
using UnityEngine;

namespace _EventBus.Scripts.Entities.Hero
{
    [Serializable]
    public class Hero
    {
        [SerializeField] public HeroType heroType;
        [SerializeField] public int damage;
        [SerializeField] public int health;
    }
}