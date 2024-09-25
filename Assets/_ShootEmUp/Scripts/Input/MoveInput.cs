using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface IMoveInput
    {
        public event Action<Vector2> OnMove;
    }
    
    public class MoveInput : IMoveInput, IFixedUpdateGameListener
    {
        public event Action<Vector2> OnMove;
        
        private void Move(float deltaTime)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Debug.Log("[MoveInput] OnMove LeftArrow");
                OnMove?.Invoke(Vector2.left * deltaTime);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                Debug.Log("[MoveInput] OnMove RightArrow");
                OnMove?.Invoke(Vector2.right * deltaTime);
            }
        }

        public void OnFixedUpdate(float deltaTime)
        {
            Move(deltaTime);
        }
    }
}