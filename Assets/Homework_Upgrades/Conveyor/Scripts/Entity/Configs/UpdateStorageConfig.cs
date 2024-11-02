using UnityEngine;

namespace Homework_Upgrades.Conveyor.Scripts.Entity.Configs
{
    public class UpdateLevelConfig : ScriptableObject
    {
        [Header("Level Up")]
        [SerializeField] public int maxLevel;
        [SerializeField] public int defaultPrice;
    }
    
    [CreateAssetMenu(
        fileName = "UpdateStorageConfig",
        menuName = "Conveyors/New UpdateStorageConfig"
    )]
    public class UpdateStorageConfig : UpdateLevelConfig
    {
        [SerializeField]
        public Sprite iconResources;
        
        [Header("Max Value")]
        [SerializeField] public int stepMaxValue;
    }
}