using System;
using UnityEngine;

namespace _EventBus.Scripts.Entities.Hero
{
    [Serializable]
    public class Hero
    {
        [SerializeField] private HeroType heroType;
        [SerializeField] private int damage;
        [SerializeField] private int health;
    }
}