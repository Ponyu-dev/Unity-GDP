using UnityEngine;

namespace _EventBus.Scripts.Entities.Player
{
    [CreateAssetMenu(menuName = "EventBus/Player", fileName = "PlayerConfig", order = 0)]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] public Player player;
    }
}