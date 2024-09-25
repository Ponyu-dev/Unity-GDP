using System;
using UnityEngine;
using VContainer;

namespace ShootEmUp
{
    public sealed class HitPointsComponent : IHitPointsComponent, ITimerGameListener
    {
        public event Action OnDeath;
        private int m_CurrentPoints;

        private readonly HitPointsData m_HitPointsData;

        [Inject]
        public HitPointsComponent(HitPointsData hitPointsData)
        {
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