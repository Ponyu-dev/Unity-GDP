using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface IShootInput
    {
        public event Action OnShoot;
    }
    
    public class ShootInput : IShootInput, IGameUpdateListener
    {
        public event Action OnShoot;
        
        private void Shoot()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("[ShootInput] OnShoot?.Invoke();");
                OnShoot?.Invoke();
            }
        }

        public void OnUpdate(float deltaTime)
        {
            Shoot();
        }
    }
}