using System;
using UnityEngine;

namespace ShootEmUp
{
    public class MoveInput : MonoBehaviour, IGameFixedUpdateListener
    {
        public event Action<Vector2> OnMove;
        
        private void Move(float deltaTime)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
                OnMove?.Invoke(Vector2.left * deltaTime);
            else if (Input.GetKey(KeyCode.RightArrow))
                OnMove?.Invoke(Vector2.right * deltaTime);
        }

        public void OnFixedUpdate(float deltaTime)
        {
            Move(deltaTime);
        }
    }
}