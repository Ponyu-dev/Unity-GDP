using System;

namespace ShootEmUp
{
    public interface IHitPointsComponent
    {
        public event Action OnDeath;
        public bool IsHitPointsExists();
        public void TakeDamage(int damage);

    }
}