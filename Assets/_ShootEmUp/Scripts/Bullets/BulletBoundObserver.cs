using System;
using UnityEngine;

namespace ShootEmUp
{
    public class BulletBoundObserver
    {
        public event Action OnExceedBounded;
        
        private readonly LevelBounds m_LevelBounds;

        public BulletBoundObserver(LevelBounds levelBounds)
        {
            m_LevelBounds = levelBounds;
        }
        
        public void CheckInBounds(Vector3 position)
        {
            if (m_LevelBounds.InBounds(position)) return;
            
            OnExceedBounded?.Invoke();
        }
    }
}