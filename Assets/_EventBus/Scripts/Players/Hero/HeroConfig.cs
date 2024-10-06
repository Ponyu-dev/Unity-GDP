using UnityEngine;

namespace _EventBus.Scripts.Players.Hero
{
    [CreateAssetMenu(menuName = "EventBus/Hero", fileName = "HeroConfig", order = 0)]
    public class HeroConfig : ScriptableObject
    {
         [SerializeField] public HeroData hero;
        [SerializeField] public Sprite heroPortrait;
    }
}