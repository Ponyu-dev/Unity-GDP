using Atomic.Entities;
using Game.Scripts.Helpers;
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
            entity.GetAnimatorDispatcher().SubscribeOnEvent(ActionType.SHOOT, OnShoot);
        }

        private void OnShoot()
        {
            _particleSystem.Play();
        }

        public void Disable(IEntity entity)
        {
            entity.GetAnimatorDispatcher().UnsubscribeOnEvent(ActionType.SHOOT, OnShoot);
        }
    }
}