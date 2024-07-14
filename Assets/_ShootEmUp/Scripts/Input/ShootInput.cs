using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class ShootInput : MonoBehaviour, IGameUpdateListener
    {
        public event Action OnShoot;
        
        private void Shoot()
        {
            if (Input.GetKeyDown(KeyCode.Space)) 
                OnShoot?.Invoke();
        }

        public void OnUpdate(float deltaTime)
        {
            Shoot();
        }
    }
}