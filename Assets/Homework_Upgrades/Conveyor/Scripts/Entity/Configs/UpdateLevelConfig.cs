using UnityEngine;

namespace Homework_Upgrades.Conveyor.Scripts.Entity.Configs
{
    public class UpdateLevelConfig : ScriptableObject
    {
        [Header("Level Up")]
        [SerializeField] public int maxLevel;
        [SerializeField] public int defaultPrice;
    }
}