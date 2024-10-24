using System;
using _ECS_RTS.Scripts.EcsEngine.Helpers;

namespace _ECS_RTS.Scripts.EcsEngine.Components
{
    [Serializable]
    public struct TeamTag
    {
        public TeamType Value;
    }
    [Serializable] public struct FactoryTag { }
    [Serializable] public struct ArrowTag { }
    [Serializable] public struct DamageableTag { }
}