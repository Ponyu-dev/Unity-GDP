using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace ShootEmUp
{
    public sealed class GameManagerContext
    {
        [ShowInInspector, ReadOnly]
        private IEnumerable<IGameListener> m_Listeners;

        /*[ShowInInspector, ReadOnly]
        private readonly IEnumerable<IGameUpdateListener> m_UpdateListeners;

        [ShowInInspector, ReadOnly]
        private readonly IEnumerable<IGameFixedUpdateListener> m_FixedUpdateListeners;

        [ShowInInspector, ReadOnly]
        private readonly IEnumerable<IGameLateUpdateListener> m_LateUpdateListeners;*/
        
        [Inject]
        private void Construct(IEnumerable<IGameListener> listeners)
        {
            m_Listeners = listeners;
            Debug.Log($"[GameManagerContext] {m_Listeners?.Count()}");
        }

        public void OnInitialize()
        {
            Debug.Log("[GameManagerContext] OnInitialize");
            foreach (var listener in m_Listeners)
            {
                if (listener is IInitializable initialize)
                    initialize.Initialize();
            }
        }

        public void OnStartTimer()
        {
            Debug.Log("[GameManagerContext] OnStartTimer");
            foreach (var listener in this.m_Listeners)
            {
                if (listener is IGameTimerListener startTimerListener)
                    startTimerListener.OnStartTimer();
            }
        }

        public void OnStartGame()
        {
            Debug.Log("[GameManagerContext] OnStartGame");

            foreach (var listener in this.m_Listeners)
            {
                if (listener is IGameStartListener startListener)
                    startListener.OnStartGame();
            }
        }

        public void OnPauseGame()
        {
            Debug.Log("[GameManagerContext] OnPauseGame");

            foreach (var listener in this.m_Listeners)
            {
                if (listener is IGamePauseListener pauseListener)
                    pauseListener.OnPauseGame();
            }
        }

        public void OnResumeGame()
        {
            Debug.Log("[GameManagerContext] OnResumeGame");

            foreach (var listener in this.m_Listeners)
            {
                if (listener is IGameResumeListener resumeListener) 
                    resumeListener.OnResumeGame();
            }
        }
        
        public void OnFinishGame()
        {
            Debug.Log("[GameManagerContext] OnFinishGame");
            
            foreach (var listener in this.m_Listeners)
            {
                if (listener is IGameFinishListener resumeListener) 
                    resumeListener.OnFinishGame();
            }
        }
    }
}