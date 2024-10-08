using UnityEngine;

namespace _EventBus.Scripts.Players.Hero
{
    [CreateAssetMenu(menuName = "EventBus/Hero", fileName = "HeroConfig", order = 0)]
    public class HeroConfig : ScriptableObject
    {
        [SerializeField] public Sprite portrait;
        [SerializeField] public HeroType type;
        [SerializeField] public int damage;
        [SerializeField] public int health;
    }
}