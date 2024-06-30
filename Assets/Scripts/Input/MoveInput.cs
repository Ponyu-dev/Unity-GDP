using System;
using UnityEngine;

namespace ShootEmUp
{
    public class MoveInput : MonoBehaviour, IMoveInput
    {
        public event Action<Vector2> OnMove;

        private void FixedUpdate()
        {
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
            OnMove?.Invoke(direction * Time.fixedDeltaTime);
        }
    }
}