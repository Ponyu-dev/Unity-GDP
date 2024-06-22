using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour, IMoveInput, IShootInput
    {
        public event Action<Vector2> OnMove;
        public event Action OnShoot;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Move(Vector2.left);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                Move(Vector2.right);
            }
        }
        
        private void Move(Vector2 direction)
        {
            OnMove?.Invoke(direction);
        }

        private void Shoot()
        {
            OnShoot?.Invoke();
        }
    }
}