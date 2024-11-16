using Atomic.Elements;
using Atomic.Entities;
using Game.Scripts.Common.Mechanics;
using Game.Scripts.Helpers;
using UnityEngine;

namespace Game.Scripts.Installers
{
    public sealed class BulletInstaller : SceneEntityInstallerBase
    {
        [SerializeField] private Cycle periodLife;
        [SerializeField] private Const<int> damage;
        [SerializeField] private Const<float> bulletSpeed;
        [SerializeField] private BaseEvent<IEntity> deadAction;
        [SerializeField] private TriggerEventReceiver triggerEventReceiver;
        [SerializeField] private Rigidbody rigidbody;
        
        public override void Install(IEntity entity)
        {
            entity.AddRootTransform(transform);
            entity.AddRigidbody(rigidbody);
            entity.AddLifeTime(periodLife);
            entity.AddDamage(damage);
            entity.AddBulletSpeed(bulletSpeed);
            entity.AddDeadAction(deadAction);
            entity.AddTriggerEventReceiver(triggerEventReceiver);
            
            entity.AddBehaviour(new TimeOfLifeBehaviour());
            entity.AddBehaviour(new TriggerEventBehaviour());
        }
    }
}