using System.Linq;
using _ECS._RTS.Scripts.Components;
using _ECS._RTS.Scripts.UI.Views;
using Cysharp.Threading.Tasks;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace _ECS._RTS.Scripts.Installers
{
    public class BaseInstaller : EntityInstaller
    {
        [SerializeField] private int defaultHealth = 200;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private HealthView healthView;

        [SerializeField] private Transform[] spawnPoints;  
        [SerializeField] private Transform container;  
        [SerializeField] private Entity[] prefabEntities;  
        [SerializeField] private Quaternion spawnRotation;  
        
        public HealthView GetHealthView() => healthView;
        public Health GetHealth() => new Health { Current = defaultHealth, Max = defaultHealth };
        
        public int EntityId { get; private set; }

        protected override void Install(Entity entity)
        {
            EntityId = entity.Id;
            entity.AddData(GetHealth());
            entity.AddData(new Base());
            entity.AddData(new Layer { Value = layerMask});
            entity.AddData(new Position { Value = transform.position});
            entity.AddData(new TransformView { Value = transform });
            
            entity.AddData(new SpawnPrefabs { Value = prefabEntities });
            entity.AddData(new SpawnPoints { Value = spawnPoints.Select(sp => sp.position).ToArray() });
            entity.AddData(new SpawnRotation() { Value = spawnRotation });
            entity.AddData(new ContainerView { Value = container });
        }

        protected override void Dispose(Entity entity) { }
    }
}