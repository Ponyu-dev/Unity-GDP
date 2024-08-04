using UnityEngine;

namespace ShootEmUp
{
    public class CharacterDeathObserver : MonoBehaviour,
        IGameStartListener,
        IGameFinishListener
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private HitPointsComponent hitPointsComponent;
        
        void IGameStartListener.OnStartGame()
        {
            this.hitPointsComponent.OnDeath += OnDeath;
        }

        void IGameFinishListener.OnFinishGame()
        {
            this.hitPointsComponent.OnDeath -= OnDeath;
        }

        private void OnDeath()
        {
            this.gameManager.FinishGame();
        }
    }
}