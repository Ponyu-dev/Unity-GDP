using UnityEngine;

namespace Homework_Upgrades.Conveyor.Scripts.Entity.Configs
{
    [CreateAssetMenu(
        fileName = "UpdateTimerConfig",
        menuName = "Conveyors/New UpdateTimerConfig"
    )]
    public class UpdateTimerConfig : UpdateLevelConfig
    {
        [Header("Timer Value")]
        [SerializeField] public float startTimerValue;
        [SerializeField] public float stepTimerValue;
        [SerializeField] public float minTimerValue;
    }
}