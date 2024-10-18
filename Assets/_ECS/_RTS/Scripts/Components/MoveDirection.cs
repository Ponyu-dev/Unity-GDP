using System;
using UnityEngine;

namespace _ECS._RTS.Scripts.Components
{
    [Serializable]
    public struct MoveDirection : IVector3Component
    {
        public Vector3 Value { get; set; }
    }
}