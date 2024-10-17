using _ECS._RTS.Scripts.Components;
using _ECS._RTS.Scripts.Enums;
using _ECS._RTS.Scripts.Services;
using _ECS._RTS.Scripts.UI.Presenters;
using _ECS._RTS.Scripts.UI.Views;
using Leopotam.EcsLite.Entities;
using UnityEngine;
using VContainer;

namespace _ECS._RTS.Scripts.Installers
{
    public class BaseInstaller : EntityInstaller
    {
        [SerializeField] private int defaultHealth = 200;
        [SerializeField] private Teams defaultTeam;
        
        [SerializeField] private HealthView healthView;
        public HealthView GetHealthView() => healthView;
        public Health GetHealth() => new Health { Current = defaultHealth, Max = defaultHealth };
        
        public int EntityId { get; private set; }

        protected override void Install(Entity entity)
        {
            EntityId = entity.Id;
            Debug.Log($"[BaseInstaller] Install({EntityId})");
            entity.AddData(GetHealth());
            entity.AddData(new Team { Value = defaultTeam });
            entity.AddData(new TransformView { Value = transform });
        }

        protected override void Dispose(Entity entity) { }
    }
}