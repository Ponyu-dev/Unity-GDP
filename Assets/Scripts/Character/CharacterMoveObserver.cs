using System;
using UnityEngine;
using Utils;

namespace ShootEmUp
{
    public class CharacterMoveObserver : MonoBehaviour
    {
        [SerializeField] private MoveInput moveInput;
        [SerializeField] private MoveComponent moveComponent;
        
        private readonly CompositeCondition m_Condition = new();
        
        public void AppendCondition(Func<bool> condition)
        {
            m_Condition.Append(condition);
        }
        
        private void OnEnable()
        {
            this.moveInput.OnMove += OnMove;
        }
    
        private void OnDisable()
        {
            this.moveInput.OnMove -= OnMove;
        }

        private void OnMove(Vector2 offset)
        {
            if (m_Condition.IsAllFalse()) return;
            
            this.moveComponent.Move(offset);
        }
    }
}