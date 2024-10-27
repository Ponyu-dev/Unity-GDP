using System;
using Leopotam.EcsLite.Entities;
using UnityEngine;
using VContainer;

namespace _ECS_RTS.Scripts.EcsEngine.Views
{
    internal sealed class CollisionComponentView : MonoBehaviour
    {
        [SerializeField] private int damage; 
        private ICollisionComponentPresenter _presenter;

        public event Action<GameObject> OnCollisionEntered;
        
        [Inject]
        public void Inject(ICollisionComponentPresenter presenter)
        {
            Debug.Log($"[CollisionComponentView] Inject {gameObject.name}");
            _presenter = presenter;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_presenter == null) return;
            if (!collision.gameObject.TryGetComponent(out Entity target)) return;
            
            Debug.Log($"[CollisionComponentView] OnCollisionEnter {gameObject.name} {gameObject.layer} {target.gameObject.layer}");
            
            if (gameObject.layer == target.gameObject.layer) return;

            OnCollisionEntered?.Invoke(gameObject);
            _presenter.OnCollisionEntered(
                new CollisionComponentData
                {
                    Damage = damage,
                    Target = target.Id,
                    PointContact = collision.GetContact(0).point
                });
        }
    }
}