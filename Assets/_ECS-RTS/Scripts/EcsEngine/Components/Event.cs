using System;

namespace _ECS_RTS.Scripts.EcsEngine.Components
{
    [Serializable] public struct AnimEvent { }
    [Serializable] public struct IdleEvent { }
    [Serializable] public struct WalkEvent { }
    [Serializable] public struct AttackEvent { }
    [Serializable] public struct TakeDamageEvent { }
    [Serializable] public struct DeathEvent { }
    [Serializable] public struct DestroyEvent { }
}