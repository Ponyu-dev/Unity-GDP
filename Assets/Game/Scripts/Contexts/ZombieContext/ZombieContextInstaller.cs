using Atomic.Contexts;
using Atomic.Elements;
using Atomic.Entities;
using Game.Scripts.Contexts.Base;
using Game.Scripts.Contexts.ZombieContext.MovementSystem;
using UnityEngine;

namespace Game.Scripts.Contexts.ZombieContext
{
    public sealed class ZombieContextInstaller : EntityContextInstaller
    {
        [SerializeField] private SceneEntity player;
        
        public override void Install(IContext context)
        {
            base.Install(context);
            context.AddAttackPlayer(new Const<IEntity>(player));
            context.AddSystem<ZombieMovementSystem>();        
        }
    }
}