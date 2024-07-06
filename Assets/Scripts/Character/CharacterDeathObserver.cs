using UnityEngine;

namespace ShootEmUp
{
    public class CharacterDeathObserver : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private HitPointsComponent hitPointsComponent;
        
        private void OnEnable()
        {
            this.hitPointsComponent.OnDeath += OnDeath;    
        }
    
        private void OnDisable()
        {
            this.hitPointsComponent.OnDeath -= OnDeath;    
        }

        private void OnDeath()
        {
            this.gameManager.StopGame();
        }
    }
}