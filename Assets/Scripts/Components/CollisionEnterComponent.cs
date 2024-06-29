using UnityEngine;

namespace ShootEmUp
{
    public interface ICollisionEnterComponent
    {
        void OnCollisionEntered(Args bulletArgs);
    }
    
    public class CollisionEnterComponent : MonoBehaviour, ICollisionEnterComponent
    {
        [SerializeField] private TeamComponent _teamComponent;
        [SerializeField] private HitPointsComponent _hitPointsComponent;

        public void OnCollisionEntered(Args bulletArgs)
        {
            if (bulletArgs._isPlayer == _teamComponent.IsPlayer) return;
            _hitPointsComponent.TakeDamage(bulletArgs._damage);
        }
    }
}