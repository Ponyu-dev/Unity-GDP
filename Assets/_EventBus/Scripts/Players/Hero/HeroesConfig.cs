using System.Collections.Generic;
using UnityEngine;

namespace _EventBus.Scripts.Players.Hero
{
    [CreateAssetMenu(menuName = "EventBus/Heroes", fileName = "_AllHeroesConfig", order = 0)]
    public class HeroesConfig : ScriptableObject
    {
        [SerializeField] private List<HeroConfig> heroes;

        public IReadOnlyList<HeroConfig> GetHeroes() => heroes;
        public HeroConfig GetHeroConfig(HeroType heroType) => heroes.Find(it => it.type == heroType);
    }
}