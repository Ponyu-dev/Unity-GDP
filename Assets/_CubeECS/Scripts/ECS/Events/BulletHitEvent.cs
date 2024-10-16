using UnityEngine;

namespace _CubeECS.Scripts.ECS.Events
{
    public struct BulletHitEvent
    {
        public int BulletEntity; // ECS-сущность пули
        public GameObject BulletGO; // ECS-сущность пули
        public int TargetEntity; // ECS-сущность юнита
        public GameObject TargetGO; // ECS-сущность юнита
    }
}