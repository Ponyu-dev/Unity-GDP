using System;
using Atomic.Entities;
using Game.Scripts.Common.Mechanics;
using Game.Scripts.Helpers;
using UnityEngine;

namespace Game.Scripts.Common.ComponentInstallers
{
    [Serializable]
    public sealed class TriggerEventComponentInstaller : IComponentInstaller
    {
        [SerializeField] private TriggerEventReceiver triggerEventReceiver;
        
        public void Install(IEntity entity)
        {
            entity.AddTriggerEventReceiver(triggerEventReceiver);
            entity.AddBehaviour(new TriggerEventBehaviour());
        }
    }
}