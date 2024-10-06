using System;
using _EventBus.Scripts.Players.Hero;
using UnityEngine;

namespace _EventBus.Scripts.Players.Player
{
    [Serializable]
    public class Player
    {
        [SerializeField] public PlayerType playerType;
        [SerializeField] public HeroConfig[] heroConfigs;
    }
}