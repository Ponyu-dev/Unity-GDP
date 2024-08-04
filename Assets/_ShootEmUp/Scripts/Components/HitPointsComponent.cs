using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class HitPointsComponent : MonoBehaviour
    {
        [SerializeField]
        private int hitPoints;

        private int m_CurrentPoints;

        public void Construct()
        {
            m_CurrentPoints = hitPoints;
        }
        
        public event Action OnDeath;
        
        public bool IsHitPointsExists() {
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
    }
}