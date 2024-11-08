using System;
using Atomic.Elements;
using Atomic.Entities;
using Game.Scripts.Common.Mechanics.Animations;
using UnityEngine;

namespace Game.Scripts.Common.Components
{
    [Serializable]
    public sealed class AnimatorComponent : IComponentInstaller
    {
        [SerializeField] private Animator animator;
        [SerializeField] private BaseEvent<string> animTriggerEvent;
        [SerializeField] private BaseEvent<string, bool> animBoolEvent;
        
        public void Install(IEntity entity)
        {
            entity.AddAnimator(animator);
            entity.AddAnimTriggerEvent(animTriggerEvent);
            entity.AddAnimBoolEvent(animBoolEvent);

            entity.AddBehaviour(new TriggerAnimatorBehaviour());
            entity.AddBehaviour(new BoolAnimatorBehaviour());
        }
    }
}