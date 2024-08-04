using UnityEngine;

namespace ShootEmUp
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        [SerializeField] private Transform firePoint;
        
        public Vector2 position => this.firePoint.position;
        public Quaternion rotation => firePoint.rotation;
    }
}