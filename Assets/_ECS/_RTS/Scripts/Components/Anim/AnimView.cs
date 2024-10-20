using System;
using _ECS._RTS.Scripts.AnimationHelper.Base;

namespace _ECS._RTS.Scripts.Components.Anim
{
    [Serializable]
    public struct AnimView
    {
        public IAnimatorCoder Value;
    }
}