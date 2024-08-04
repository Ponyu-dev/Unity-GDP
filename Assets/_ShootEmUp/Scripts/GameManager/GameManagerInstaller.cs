using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(GameManager))]
    public sealed class GameManagerInstaller : MonoBehaviour
    {
        [SerializeField]
        private GameObject ui;

        private GameManager m_GameManager; 
        
        private void Awake()
        {
            m_GameManager = this.GetComponent<GameManager>();

            AddListener(this.GetComponentsInChildren<IGameListener>());
            AddListener(ui.GetComponentsInChildren<IGameListener>());
        }

        private void AddListener(IEnumerable<IGameListener> gameListeners)
        {
            foreach (var listener in gameListeners)
            {
                m_GameManager.AddListener(listener);
            }
        }
    }
}