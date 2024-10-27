using System;
using System.Collections.Generic;
using _ECS_RTS.Scripts.EcsEngine.Helpers.Pools;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _ECS_RTS.Scripts.EcsEngine.Services
{
    public enum SfxType
    {
        Blood,
        BuildingBurning,
        BuildingBurningSmall,
        BuildingDestroyed,
    }
    
    public interface ISfxFactory
    {
        void PlaySfx(SfxType sfxType, Vector3 position);
    }
    
    internal sealed class SfxFactory : ISfxFactory, IStartable, IDisposable
    {
        private static int DEFAULT_SIZE = 5;
        private readonly Dictionary<SfxType, PoolComponent> _poolDictionary = new();

        [Inject]
        public SfxFactory(
            Transform container,
            Transform worldTransform,
            bool autoExpand,
            Dictionary<SfxType, GameObject> prefabs)
        {
            Debug.Log($"[SfxFactory] Constructor");
            foreach (var prefab in prefabs)
            {
                var pool = new PoolComponent(default, prefab.Value, container, worldTransform, autoExpand);
                _poolDictionary.Add(prefab.Key, pool);
            }
        }

        public void Start()
        {
            Debug.Log($"[SfxFactory] Start");
            for (var i = 0; i < DEFAULT_SIZE; i++)
            {
                foreach (var pool in _poolDictionary.Values)
                {
                    pool.CreateObject(isEnqueue: true);
                }
            }
        }
        
        public void PlaySfx(SfxType sfxType, Vector3 position)
        {
            if (!_poolDictionary.TryGetValue(sfxType, out var pool)) return;
            if (!pool.TryGet(out var gameObject)) return;

            pool.ActiveObject(gameObject);
            gameObject.transform.position = position;
            
            if (!gameObject.TryGetComponent<ParticleSystem>(out var sfx))
                return;
            
            sfx.Play();
            
            if (sfxType is SfxType.Blood or SfxType.BuildingDestroyed) 
                HandleParticleCompletionAsync(sfx, gameObject, sfxType);
        }
        
        private async UniTaskVoid HandleParticleCompletionAsync(ParticleSystem sfx, GameObject go, SfxType sfxType)
        {
            // Ждем завершения партикла
            try
            {
                await UniTask.WaitUntil(() => !sfx.IsAlive(true));
                _poolDictionary[sfxType].InactiveObject(go);
            }
            catch (Exception excp)
            {
                Debug.LogException(excp);
            }
        }

        public void Dispose()
        {
            foreach (var pool in _poolDictionary.Values)
            {
                pool.ClearPool();
            }
        }
    }
}