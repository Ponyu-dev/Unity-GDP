using System;
using UnityEngine;

namespace _ECS._RTS.Scripts.AnimationHelper.Base
{
    /// <summary> Allows the animation parameters to be shown in debug inspector </summary>
    [Serializable]
    public struct ParameterDisplay
    {
        [HideInInspector] public string name;
        public bool value;
    }
}