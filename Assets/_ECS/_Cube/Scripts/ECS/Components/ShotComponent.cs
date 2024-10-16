using System.Collections.Generic;
using CubeECS.Scripts.ECS.Utils;
using UnityEngine;

namespace CubeECS.Scripts.ECS.Components
{
    public struct ShotComponent
    {
        public Team Team;
        public float LastShootTime;
        public float ShootCooldown;
        public List<Collider> CollidersEnemy;
        public float BulletSpeed;
    }
}