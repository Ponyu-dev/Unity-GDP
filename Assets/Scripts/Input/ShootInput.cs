using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class ShootInput : MonoBehaviour, IShootInput
    {
        public event Action OnShoot;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }
        }
        
        private void Shoot()
        {
            OnShoot?.Invoke();
        }
    }
}