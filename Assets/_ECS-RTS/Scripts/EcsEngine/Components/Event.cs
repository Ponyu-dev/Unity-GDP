using System;

namespace _ECS_RTS.Scripts.EcsEngine.Components
{
    [Serializable] public struct IdleEvent { }
    [Serializable] public struct WalkEvent { }
    [Serializable] public struct RunEvent { }
    [Serializable] public struct AttackEvent { }
    [Serializable] public struct TakeDamageEvent { }
    [Serializable] public struct DeathEvent { }
}