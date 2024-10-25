using System;
using UnityEngine;

namespace _ECS_RTS.Scripts.EcsEngine.Components
{
    [Serializable]
    public struct MoveSpeed
    {
        public float Value;
    }
    
    [Serializable]
    public struct MoveDirection
    {
        public Vector3 Value;
    }
    
    [Serializable]
    public struct MoveTarget : ITargetEntity
    {
        public int Value { get; set; } 
    }
}