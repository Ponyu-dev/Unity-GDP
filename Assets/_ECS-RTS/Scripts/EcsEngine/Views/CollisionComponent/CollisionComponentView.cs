using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace _ECS_RTS.Scripts.EcsEngine.Views
{
    internal sealed class CollisionComponentView : MonoBehaviour
    {
        [SerializeField] private int damage; 
        private ICollisionComponentPresenter _presenter;
        
        public void Inject(ICollisionComponentPresenter presenter)
        {
            _presenter = presenter;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_presenter == null) return;
            if (!collision.gameObject.TryGetComponent(out Entity target)) return;
            if (gameObject.layer == target.gameObject.layer) return;
            
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