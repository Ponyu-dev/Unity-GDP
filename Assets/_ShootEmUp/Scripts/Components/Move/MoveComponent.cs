using UnityEngine;
using VContainer;

namespace ShootEmUp
{
    public sealed class MoveComponent : IMoveComponent
    {
        private MoveData m_MoveData;

        [Inject]
        public void Construct(MoveData moveData)
        {
            Debug.Log("[MoveComponent] Construct");
            m_MoveData = moveData;
        }
        
        public void Move(Vector2 offset)
        {
            var nextPosition = m_MoveData.CurrentPosition + offset * m_MoveData.Speed;
            m_MoveData.GetRigidbody2D().MovePosition(nextPosition);
        }
    }
}