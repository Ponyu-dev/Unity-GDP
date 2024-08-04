using UnityEngine;

namespace ShootEmUp
{
    public sealed class MoveComponent : MonoBehaviour
    {
        [SerializeField] private Vector3 defaultPosition = new Vector3(0f, -3.5f, 0f);
        [SerializeField] private new Rigidbody2D rigidbody2D;
        [SerializeField] private float speed = 5.0f;

        public void SetDefaultPosition()
        {
            transform.position = defaultPosition;
        }
        
        public void Move(Vector2 offset)
        {
            var nextPosition = this.rigidbody2D.position + offset * this.speed;
            this.rigidbody2D.MovePosition(nextPosition);
        }
    }
}