using UnityEngine;

namespace ShootEmUp
{
    public class Character : MonoBehaviour,
        IGameTimerListener
    {
        [SerializeField] private CharacterShootObserver characterShootObserver;
        [SerializeField] private CharacterMoveObserver characterMoveObserver;
        [SerializeField] private HitPointsComponent hitPointsComponent;
        [SerializeField] private MoveComponent moveComponent;
        
        private void Start()
        {
            characterShootObserver.AppendCondition(hitPointsComponent.IsHitPointsExists);
            characterMoveObserver.AppendCondition(hitPointsComponent.IsHitPointsExists);
        }

        public void OnStartTimer()
        {
            hitPointsComponent.Construct();
            moveComponent.SetDefaultPosition();
        }
    }
}