using UnityEngine;
using VContainer;

namespace ShootEmUp
{
    public class DamageComponent : MonoBehaviour
    {
        private TeamData m_TeamData;
        private IHitPointsComponent m_HitPointsComponent;

        [Inject]
        public void Construct(IHitPointsComponent hitPointsComponent, TeamData teamData)
        {
            Debug.Log("[DamageComponent] Construct");
            m_HitPointsComponent = hitPointsComponent;
            m_TeamData = teamData;
        }

        public void OnDamage(BulletData bulletData)
        {
            if (bulletData.isPlayer == m_TeamData.IsPlayer) return;
            m_HitPointsComponent.TakeDamage(bulletData.damage);
        }
    }
}