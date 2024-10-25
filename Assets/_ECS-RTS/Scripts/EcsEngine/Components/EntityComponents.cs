using System;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace _ECS_RTS.Scripts.EcsEngine.Components
{
    [Serializable] public struct Inactive { }
    [Serializable] public struct OneFrame { }
    
    [Serializable]
    public struct Position
    {
        public Vector3 Value;
    }
    
    [Serializable]
    public struct Rotation
    {
        public Quaternion Value;
    }
    
    [Serializable]
    public struct Prefab
    {
        public Entity Value;
    }
    
    [Serializable]
    public struct Health
    {
        public int Value;
    }
    
    [Serializable]
    public struct SourceEntity : ITargetEntity
    {
        public int Value { get; set; } 
    }
    
    [Serializable]
    public struct TargetEntity : ITargetEntity
    {
        public int Value { get; set; } 
    }
}