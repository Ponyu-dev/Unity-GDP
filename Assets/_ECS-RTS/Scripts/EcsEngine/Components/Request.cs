using System;

namespace _ECS_RTS.Scripts.EcsEngine.Components
{
    [Serializable] public struct SpawnRequest { }
    [Serializable] public struct CollisionEnterRequest { }
    [Serializable] public struct ArrowRequest { }
    [Serializable] public struct DeathRequest { }
    [Serializable] public struct TakeDamageRequest { }
    [Serializable] public struct FirstTargetSelectedRequest { }
}