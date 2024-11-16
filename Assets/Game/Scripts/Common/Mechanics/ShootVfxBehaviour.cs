using Atomic.Entities;
using UnityEngine;

namespace Game.Scripts.Common.Mechanics
{
    public sealed class ShootVfxBehaviour : IEntityInit, IEntityEnable, IEntityDisable
    {
        private ParticleSystem _particleSystem;
        
        public void Init(IEntity entity)
        {
            _particleSystem = entity.GetShootVFX();
        }

        public void Enable(IEntity entity)
        {
            entity.GetShootAnimationReceiver().OnShoot += OnShoot;
        }

        private void OnShoot()
        {
            _particleSystem.Play();
        }

        public void Disable(IEntity entity)
        {
            entity.GetShootAnimationReceiver().OnShoot -= OnShoot;
        }
    }
}