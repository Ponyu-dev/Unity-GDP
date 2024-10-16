using CubeECS.Scripts.ECS.Utils;
using UnityEngine;

namespace CubeECS.Scripts.ECS.Components
{
    public struct BulletComponent
    {
        public Team Team; // Команда (красная или синяя)
        public float Speed; // Скорость полета
        public GameObject BulletObject; 
    }
}