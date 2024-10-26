using _ECS_RTS.Scripts.EcsEngine.Components;
using UnityEngine;
using VContainer;

namespace _ECS_RTS.Scripts.EcsEngine.Views
{
    public struct CollisionComponentData
    {
        public int Damage;
        public int Target;
        public Vector3 PointContact;
    }
    
    public interface ICollisionComponentPresenter
    {
        void OnCollisionEntered(CollisionComponentData data);
    }
    
    public class CollisionComponentPresenter : ICollisionComponentPresenter
    {
        private readonly IEcsStartup _ecsStartup;
        
        [Inject]
        public CollisionComponentPresenter(IEcsStartup ecsStartup)
        {
            _ecsStartup = ecsStartup;
        }
        
        public void OnCollisionEntered(CollisionComponentData data)
        {
            _ecsStartup.CreateEntity(EcsWorlds.EVENTS)
                .Add(new CollisionEnterRequest())
                .Add(new CollisionEnterTag())
                .Add(new Damage { Value = data.Damage })
                .Add(new TargetEntity { Value = data.Target })
                .Add(new Position { Value = data.PointContact });
        }
    }
}