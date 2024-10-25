using System;
using UnityEngine;

namespace _ECS_RTS.Scripts.EcsEngine.Components
{
    [Serializable]
    public struct AnimatorView
    {
        public Animator Value;
    }
    
    [Serializable]
    public struct AttackLayerMaskView
    {
        public LayerMask Value;
    }
    
    [Serializable]
    public struct TransformView
    {
        public Transform Value;
    }
}