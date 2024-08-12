using System;
using UnityEngine;
using Utils;

namespace ShootEmUp
{
    public class CharacterMoveObserver : MonoBehaviour,
        IGameStartListener,
        IGameFinishListener
    {
        [SerializeField] private MoveInput moveInput;
        [SerializeField] private MoveComponent moveComponent;
        
        private readonly CompositeCondition m_Condition = new();
        
        public void AppendCondition(Func<bool> condition)
        {
            m_Condition.Append(condition);
        }
        
        void IGameStartListener.OnStartGame()
        {
            this.moveInput.OnMove += OnMove;
        }

        void IGameFinishListener.OnFinishGame()
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