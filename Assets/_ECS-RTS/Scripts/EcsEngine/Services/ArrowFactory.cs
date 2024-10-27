using System;
using _ECS_RTS.Scripts.EcsEngine.Helpers;
using _ECS_RTS.Scripts.EcsEngine.Helpers.Pools;
using _ECS_RTS.Scripts.EcsEngine.Views;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _ECS_RTS.Scripts.EcsEngine.Services
{
    public interface IArrowFactory
    {
        void FireArrow(TeamType teamType, Vector3 position, Vector3 moveDirection);
    }
    
    internal sealed class ArrowFactory : IArrowFactory, IStartable, IDisposable
    {
        private static readonly int DefaultSize = 10;
        private static readonly float ForceAmount = 5f;
        private static readonly int LifeTime = 3000;
        
        private readonly PoolComponent _pool;
        
        [Inject]
        public ArrowFactory(
            Transform container,
            Transform worldTransform,
            bool autoExpand,
            GameObject prefabArrow,
            IObjectResolver resolver)
        {
            Debug.Log($"[ArrowFactory] Constructor");
            _pool = new PoolComponent(resolver, prefabArrow, container, worldTransform, autoExpand);
        }
        
        public void Start()
        {
            for (var i = 0; i < DefaultSize; i++)
            {
                _pool.CreateObject();
            }
        }

        public void FireArrow(TeamType teamType, Vector3 position, Vector3 moveDirection)
        {
            if (!_pool.TryGet(out var arrow)) return;
            
            arrow.layer = LayerMask.NameToLayer(teamType.ToString());
            
            _pool.ActiveObject(arrow);

            arrow.transform.position = position;
            arrow.transform.rotation = Quaternion.LookRotation(moveDirection) * Quaternion.Euler(90, 180, 0);
            
            if (!arrow.TryGetComponent<Rigidbody>(out var rigidbody)) return;
            
            rigidbody.AddForce(moveDirection.normalized * ForceAmount, ForceMode.Impulse);
            
            DestroyAfterTimeAsync(arrow).Forget();
        }
        
        private async UniTaskVoid DestroyAfterTimeAsync(GameObject arrow)
        {
            await UniTask.Delay(LifeTime);
            arrow.layer = 0;
            _pool.InactiveObject(arrow);
        }
        
        public void Dispose()
        {
            _pool.ClearPool();
        }
    }
}