using Atomic.Contexts;
using Atomic.Elements;
using Atomic.Entities;
using Game.Scripts.Contexts.Base.EntityPool;
using Game.Scripts.Contexts.BulletContext;
using UnityEngine;

namespace Game.Scripts.Contexts.GameContext.BulletContext
{
    public class BulletContextInstaller : SceneContextInstallerBase
    {
        [SerializeField] private SceneEntity playerEntity;
        [SerializeField] private SceneEntity bullet;
        [SerializeField] private Transform container;
        [SerializeField] private Transform wTransform;
        [SerializeField] private int maxCount;
        
        public override void Install(IContext context)
        {
            context.AddPlayer(new Const<IEntity>(playerEntity));
            context.AddBulletPool(new ScenePool(
                prefab: bullet,
                poolContainer: container,
                worldContainer: wTransform,
                initialCount: maxCount));

            context.AddSystem(new BulletSpawnSystem());
        }
    }
}