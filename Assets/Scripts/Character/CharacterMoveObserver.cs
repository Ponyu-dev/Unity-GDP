using UnityEngine;

namespace ShootEmUp
{
    public class CharacterMoveObserver : MonoBehaviour
    {
        [SerializeField] private MoveInput _moveInput;
        [SerializeField] private MoveComponent _moveComponent;
        
        private void OnEnable()
        {
            this._moveInput.OnMove += OnMove;
        }
    
        private void OnDisable()
        {
            this._moveInput.OnMove -= OnMove;
        }

        private void OnMove(Vector2 offset)
        {
            this._moveComponent.Move(offset);
        }
    }
}