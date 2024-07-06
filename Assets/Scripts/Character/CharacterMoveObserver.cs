using UnityEngine;

namespace ShootEmUp
{
    public class CharacterMoveObserver : MonoBehaviour
    {
        [SerializeField] private MoveInput moveInput;
        [SerializeField] private MoveComponent moveComponent;
        
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
            this.moveComponent.Move(offset);
        }
    }
}