using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utils;
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
    
    internal sealed class SfxFactory : ISfxFactory, IStartable
    {
        private static int DEFAULT_SIZE = 5;
        private readonly Dictionary<SfxType, PoolGO> _poolDictionary = new();

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
                var pool = new PoolGO(prefab.Value, container, worldTransform, autoExpand);
                _poolDictionary.Add(prefab.Key, pool);
            }
        }

        public void Start()
        {
            Debug.Log($"[SfxFactory] Start");
            foreach (var pool in _poolDictionary.Values)
            {
                pool.CreatePool(DEFAULT_SIZE);
            }
        }
        
        public void PlaySfx(SfxType sfxType, Vector3 position)
        {
            if (!_poolDictionary.TryGetValue(sfxType, out var pool)) return;
            if (!pool.TryGet(out var sfx)) return;

            sfx.transform.position = position;
            // Получаем ParticleSystem из GameObject
            var particleSystem = sfx.GetComponent<ParticleSystem>();
            if (particleSystem == null) return;

            // Запускаем партикл с начала
            particleSystem.Play();

            // Используем UniTask для отслеживания завершения
            if (sfxType == SfxType.Blood || sfxType == SfxType.BuildingDestroyed) 
                HandleParticleCompletionAsync(particleSystem, sfx, sfxType).Forget();
        }
        
        private async UniTaskVoid HandleParticleCompletionAsync(ParticleSystem particleSystem, GameObject sfx, SfxType sfxType)
        {
            // Ждем завершения партикла
            await UniTask.WaitUntil(() => !particleSystem.IsAlive(true));
            
            _poolDictionary[sfxType].InactiveObject(sfx);
        }
    }
}