using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : MonoBehaviour, IGameFixedUpdateListener
    {
        [SerializeField] private MoveComponent moveComponent;
        
        public bool IsReached() => this.m_IsReached;
        
        private bool m_IsReached;
        private Vector2 m_Destination;

        public void SetDestination(Vector2 endPoint)
        {
            this.m_Destination = endPoint;
            this.m_IsReached = false;
        }

        public void OnFixedUpdate(float deltaTime)
        {
            if (this.m_IsReached)
            {
                return;
            }
            
            var vector = this.m_Destination - (Vector2) this.transform.position;
            if (vector.magnitude <= 0.25f)
            {
                this.m_IsReached = true;
                return;
            }

            var direction = vector.normalized * deltaTime;
            this.moveComponent.Move(direction);
        }
    }
}