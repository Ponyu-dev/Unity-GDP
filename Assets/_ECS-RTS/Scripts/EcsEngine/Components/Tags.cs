using System;
using _ECS_RTS.Scripts.EcsEngine.Helpers;

namespace _ECS_RTS.Scripts.EcsEngine.Components
{
    [Serializable] public struct TeamTag { public TeamType Value; }
    [Serializable] public struct EntityTag { public EntityType Value; }
    [Serializable] public struct CollisionEnterTag { }
    [Serializable] public struct FactoryTag { }
    [Serializable] public struct ArrowTag { }
    [Serializable] public struct DamageableTag { }
    [Serializable] public struct MoveTag { }
    [Serializable] public struct AttackTag { }
}