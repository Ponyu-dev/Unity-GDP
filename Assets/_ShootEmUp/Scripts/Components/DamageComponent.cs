using UnityEngine;

namespace ShootEmUp
{
    public class DamageComponent : MonoBehaviour
    {
        [SerializeField] private TeamComponent teamComponent;
        [SerializeField] private HitPointsComponent hitPointsComponent;

        public void OnDamage(BulletData bulletData)
        {
            if (bulletData.isPlayer == teamComponent.IsPlayer) return;
            hitPointsComponent.TakeDamage(bulletData.damage);
        }
    }
}