using UnityEngine;

namespace ShootEmUp
{
    public class CharacterDeathObserver : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private HitPointsComponent _hitPointsComponent;
        
        private void OnEnable()
        {
            this._hitPointsComponent.OnDeath += OnDeath;    
        }
    
        private void OnDisable()
        {
            this._hitPointsComponent.OnDeath -= OnDeath;    
        }

        private void OnDeath(GameObject gameObject)
        {
            this._gameManager.StopGame();
        }
    }
}