using _EventBus.Scripts.Players.Hero;
using UnityEngine;

namespace _EventBus.Scripts.Players.Player
{
    [CreateAssetMenu(menuName = "EventBus/Player", fileName = "PlayerConfig", order = 0)]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] public PlayerType playerType;
        [SerializeField] public HeroType[] heroTypes;
    }
}