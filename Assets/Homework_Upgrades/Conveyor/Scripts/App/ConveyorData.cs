using System;
using UnityEngine;

namespace Homework_Upgrades.Conveyor.Scripts.App
{
    [Serializable]
    public struct ConveyorData
    {
        [SerializeField] public string id;
        [SerializeField] public int inputAmount;
        [SerializeField] public int outputAmount;
        [SerializeField] public float timeWork;
    }
}