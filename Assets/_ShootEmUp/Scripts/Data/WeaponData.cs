using System;
using UnityEngine;

namespace ShootEmUp
{
    [Serializable]
    public sealed class WeaponData
    {
        [SerializeField] private Transform firePoint;
        
        public Vector2 position => this.firePoint.position;
        public Quaternion rotation => firePoint.rotation;
    }
}