using UnityEngine;

namespace ShootEmUp
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private CharacterShootObserver characterShootObserver;
        [SerializeField] private CharacterMoveObserver characterMoveObserver;
        [SerializeField] private HitPointsComponent hitPointsComponent;

        private void Start()
        {
            characterShootObserver.AppendCondition(hitPointsComponent.IsHitPointsExists);
            characterMoveObserver.AppendCondition(hitPointsComponent.IsHitPointsExists);
        }
    }
}