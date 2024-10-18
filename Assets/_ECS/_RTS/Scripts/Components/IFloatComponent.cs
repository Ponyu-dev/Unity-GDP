using UnityEngine;

namespace _ECS._RTS.Scripts.Components
{
    public interface IFloatComponent
    {
        float Value { get; }
    }
    
    public interface IVector3Component
    {
        Vector3 Value { get; set; }
    }
}