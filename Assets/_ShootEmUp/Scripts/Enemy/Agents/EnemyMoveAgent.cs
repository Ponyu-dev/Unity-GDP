using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : IFixedUpdateGameListener
    {
        private readonly MoveComponent m_MoveComponent;
        private readonly Transform m_Transform;
        private readonly Vector2 m_Destination;
        
        public bool IsReached() => this.m_IsReached;
        private bool m_IsReached;

        public EnemyMoveAgent(MoveComponent moveComponent, Transform transform, Vector2 endPoint)
        {
            m_MoveComponent = moveComponent;
            m_Transform = transform;
            m_Destination = endPoint;
            m_IsReached = false;
        }

        public void OnFixedUpdate(float deltaTime)
        {
            if (this.m_IsReached)
                return;
            
            var vector = this.m_Destination - (Vector2) m_Transform.position;
            if (vector.magnitude <= 0.25f)
            {
                this.m_IsReached = true;
                return;
            }

            var direction = vector.normalized * deltaTime;
            this.m_MoveComponent.Move(direction);
        }
    }
}