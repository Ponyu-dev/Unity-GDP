using UnityEngine;

namespace Homework_Upgrades.Conveyor.Scripts.Entity.Configs
{
    [CreateAssetMenu(
        fileName = "ConveyorConfig",
        menuName = "Conveyors/New ConveyorConfig"
    )]
    public class ConveyorConfig : ScriptableObject
    {
        [Header("Load Zone"), SerializeField]
        public UpdateStorageConfig loadStorageConfig;
        
        [Header("UnLoad Zone"), SerializeField]
        public UpdateStorageConfig unLoadStorageConfig;

        [Header("Work")]
        [SerializeField]
        public UpdateTimerConfig updateTimerConfig;
    }
}