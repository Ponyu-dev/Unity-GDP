using System;
using UnityEngine;

namespace ShootEmUp
{
    public class BulletBoundObserver : MonoBehaviour
    {
        public event Action OnExceedBounded;
        
        private LevelBounds m_LevelBounds;

        public void Construct(LevelBounds levelBounds)
        {
            m_LevelBounds = levelBounds;
        }
        
        private void FixedUpdate()
        {
            if (m_LevelBounds.InBounds(transform.position)) return;
            
            OnExceedBounded?.Invoke();
        }
    }
}