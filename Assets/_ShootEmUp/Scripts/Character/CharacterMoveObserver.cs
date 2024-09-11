using System;
using UnityEngine;
using Utils;
using VContainer;

namespace ShootEmUp
{
    public class CharacterMoveObserver :
        IStartGameListener,
        IFinishGameListener
    {
        private readonly CompositeCondition m_Condition = new();
        
        private IMoveInput m_MoveInput;
        private IMoveComponent m_MoveComponent;

        [Inject]
        public void Construct(
            IMoveInput moveInput,
            IMoveComponent moveComponent,
            IHitPointsComponent hitPointsComponent)
        {
            Debug.Log("[CharacterMoveObserver] Construct");
            
            m_MoveInput = moveInput;
            m_MoveComponent = moveComponent;
            AppendCondition(hitPointsComponent.IsHitPointsExists);
        }
        
        private void AppendCondition(Func<bool> condition)
        {
            m_Condition.Append(condition);
        }
        
        void IStartGameListener.OnStartGame()
        {
            this.m_MoveInput.OnMove += OnMove;
        }

        void IFinishGameListener.OnFinishGame()
        {
            this.m_MoveInput.OnMove -= OnMove;
        }

        private void OnMove(Vector2 offset)
        {
            if (m_Condition.IsAllFalse()) return;
            
            this.m_MoveComponent.Move(offset);
        }
    }
}