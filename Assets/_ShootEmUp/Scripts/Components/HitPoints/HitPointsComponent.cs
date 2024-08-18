using System;
using UnityEngine;
using VContainer;

namespace ShootEmUp
{
    public sealed class HitPointsComponent : IHitPointsComponent, IGameTimerListener
    {
        public event Action OnDeath;
        private int m_CurrentPoints;

        private HitPointsData m_HitPointsData;

        [Inject]
        public void Construct(HitPointsData hitPointsData)
        {
            Debug.Log("[HitPointsComponent] Construct");
            m_HitPointsData = hitPointsData;
        }

        private void Init()
        {
            m_CurrentPoints = m_HitPointsData.HitPoints();
        }
        
        public bool IsHitPointsExists()
        {
            return m_CurrentPoints > 0;
        }

        public void TakeDamage(int damage)
        {
            Debug.Log("[HitPointsComponent] TakeDamage");
            m_CurrentPoints -= damage;
            if (m_CurrentPoints <= 0)
            {
                this.OnDeath?.Invoke();
            }
        }

        public void OnStartTimer()
        {
            Init();
        }
    }
}