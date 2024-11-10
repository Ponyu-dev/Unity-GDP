using Atomic.Contexts;
using Atomic.Entities;
using Game.Scripts.Contexts.Base.EntityPool;
using UnityEngine;

namespace Game.Scripts.Contexts.BulletContext
{
    public sealed class BulletSpawnSystem : IContextInit, IContextEnable, IContextDisable
    {
        private IEntityPool _pool;
        
        public void Init(IContext context)
        {
            _pool = context.GetBulletPool();
        }

        public void Enable(IContext context)
        {
            context.GetPlayer().Value.GetShootAction().Subscribe(OnShoot);
        }

        private void OnShoot(Transform firePoint)
        {
            var bullet = _pool.Rent();
            bullet.GetRootTransform().position = firePoint.position;
            bullet.GetRigidbody().velocity = firePoint.forward * bullet.GetBulletSpeed().Value;
            bullet.GetLifeTime().Start();
            bullet.GetLifeTime().Play();
            bullet.GetDeadAction().Subscribe(OnBulletDead);
        }

        private void OnBulletDead(IEntity bullet)
        {
            bullet.GetDeadAction().Unsubscribe(OnBulletDead);
            _pool.Return(bullet);
        }

        public void Disable(IContext context)
        {
            context.GetPlayer().Value.GetShootAction().Unsubscribe(OnShoot);
        }
    }
}