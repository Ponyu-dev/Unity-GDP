using System;
using Atomic.Entities;
using Game.Scripts.Common.Mechanics;
using Game.Scripts.Common.Trigger;
using UnityEngine;

namespace Game.Scripts.Common.Components
{
    [Serializable]
    public sealed class TriggerEventComponent : IComponentInstaller
    {
        [SerializeField] private TriggerEventReceiver triggerEventReceiver;
        
        public void Install(IEntity entity)
        {
            entity.AddTriggerEventReceiver(triggerEventReceiver);
            entity.AddBehaviour(new TriggerEventBehaviour());
        }
    }
}