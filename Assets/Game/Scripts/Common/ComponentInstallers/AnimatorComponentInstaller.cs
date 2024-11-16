using System;
using Atomic.Elements;
using Atomic.Entities;
using Game.Scripts.Common.Mechanics.Animations;
using Game.Scripts.Helpers;
using UnityEngine;

namespace Game.Scripts.Common.ComponentInstallers
{
    [Serializable]
    public sealed class AnimatorComponentInstaller : IComponentInstaller
    {
        [SerializeField] private Animator animator;
        [SerializeField] private AnimatorDispatcher animatorDispatcher;
        [SerializeField] private BaseEvent<string> animTriggerEvent;
        [SerializeField] private BaseEvent<string, bool> animBoolEvent;
        
        public void Install(IEntity entity)
        {
            entity.AddAnimator(animator);
            entity.AddAnimTriggerEvent(animTriggerEvent);
            entity.AddAnimBoolEvent(animBoolEvent);
            entity.AddAnimatorDispatcher(animatorDispatcher);

            entity.AddBehaviour(new TriggerAnimatorBehaviour());
            entity.AddBehaviour(new BoolAnimatorBehaviour());
        }
    }
}