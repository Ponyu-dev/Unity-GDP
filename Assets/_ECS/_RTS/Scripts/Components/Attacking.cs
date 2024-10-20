using System;
using UnityEngine;

namespace _ECS._RTS.Scripts.Components
{
    [Serializable]
    public struct Attacking
    {
        public bool Value;
        public Transform Target;
    }
}