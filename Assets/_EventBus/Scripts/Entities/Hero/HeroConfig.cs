using UnityEngine;

namespace _EventBus.Scripts.Entities.Hero
{
    [CreateAssetMenu(menuName = "EventBus/Hero", fileName = "HeroConfig", order = 0)]
    public class HeroConfig : ScriptableObject
    {
        [SerializeField] private Hero hero;
        [SerializeField] private Sprite heroPortrait;
    }
}