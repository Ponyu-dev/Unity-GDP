using System;
using _EventBus.Scripts.Entities.Hero;
using UnityEngine;

namespace _EventBus.Scripts.Entities.Player
{
    [Serializable]
    public class Player
    {
        [SerializeField] public PlayerType playerType;
        [SerializeField] public HeroConfig[] heroConfigs;
    }
}