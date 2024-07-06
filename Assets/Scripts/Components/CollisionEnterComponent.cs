using UnityEngine;

namespace ShootEmUp
{
    public class CollisionEnterComponent : MonoBehaviour
    {
        [SerializeField] private TeamComponent teamComponent;
        [SerializeField] private HitPointsComponent hitPointsComponent;

        public void OnCollisionEntered(BulletData bulletData)
        {
            if (bulletData.isPlayer == teamComponent.IsPlayer) return;
            hitPointsComponent.TakeDamage(bulletData.damage);
        }
    }
}