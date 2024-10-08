using UnityEngine;
using VContainer;

namespace ShootEmUp
{
    public class CharacterDeathObserver :
        IStartGameListener,
        IFinishGameListener
    {
        private IGameManager m_GameManager;
        private IHitPointsComponent m_HitPointsComponent;

        [Inject]
        public void Construct(IGameManager gameManager, IHitPointsComponent hitPointsComponent)
        {
            Debug.Log("[CharacterDeathObserver] Construct");
            m_GameManager = gameManager;
            m_HitPointsComponent = hitPointsComponent;
        }
        
        void IStartGameListener.OnStartGame()
        {
            this.m_HitPointsComponent.OnDeath += OnDeath;
        }

        void IFinishGameListener.OnFinishGame()
        {
            this.m_HitPointsComponent.OnDeath -= OnDeath;
        }

        private void OnDeath()
        {
            this.m_GameManager.FinishGame();
        }
    }
}