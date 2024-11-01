using Homework_Upgrades.Conveyor.Scripts.Helpers;
using UnityEngine;

namespace Game.GamePlay.Conveyor
{
    [CreateAssetMenu(
        fileName = "ScriptableConveyor",
        menuName = "Conveyors/New ScriptableConveyor"
    )]
    public class ScriptableConveyor : ScriptableObject
    {
        [SerializeField]
        public string id;
        
        [Header("Load Zone")]
        [SerializeField]
        public ResourceType inputResourceType;

        [SerializeField]
        public int inputCapacity;

        [Header("Unload Zone")]
        [SerializeField]
        public ResourceType outputResourceType;

        [SerializeField]
        public int outputCapacity;

        [Header("Work")]
        [SerializeField]
        public float workTime;
    }
}