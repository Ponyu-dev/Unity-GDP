using System;

namespace _ECS_RTS.Scripts.EcsEngine.Components
{
    [Serializable]
    public struct AttackTargetEntity : ITargetEntity
    {
        public int Value { get; set; } 
    }
    
    [Serializable]
    public struct Damage
    {
        public int Value;
    }
}