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
        private List<IGameListener> m_Listeners = new();

        [ShowInInspector, ReadOnly]
        private List<IGameUpdateListener> m_UpdateListeners = new();

        [ShowInInspector, ReadOnly]
        private List<IGameFixedUpdateListener> m_FixedUpdateListeners = new();

        [ShowInInspector, ReadOnly]
        private List<IGameLateUpdateListener> m_LateUpdateListeners= new();
        
        [Inject]
        private void Construct(IEnumerable<IGameListener> listeners)
        {
            var listListeners = listeners.ToList();
            for (int i = 0, count = listListeners.Count; i < count; i++)
            {
                AddListener(listListeners[i]);
            }
        }
        
        private void AddListener(IGameListener listener)
        {
            if (listener == null) return;

            if (listener is IGameUpdateListener updateListener)
                this.m_UpdateListeners.Add(updateListener);
            
            if (listener is IGameFixedUpdateListener fixedUpdateListener)
                this.m_FixedUpdateListeners.Add(fixedUpdateListener);
            
            if (listener is IGameLateUpdateListener lateUpdateListener)
                this.m_LateUpdateListeners.Add(lateUpdateListener);
            
            this.m_Listeners.Add(listener);
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

        public void OnTick(float deltaTime)
        {
            for (int i = 0, count = this.m_UpdateListeners.Count; i < count; i++)
            {
                this.m_UpdateListeners[i]?.OnUpdate(deltaTime);
            }
        }

        public void OnFixedTick(float deltaTime)
        {
            for (int i = 0, count = this.m_FixedUpdateListeners.Count; i < count; i++)
            {
                this.m_FixedUpdateListeners[i]?.OnFixedUpdate(deltaTime);
            }
        }

        public void OnLateTick(float deltaTime)
        {
            for (int i = 0, count = this.m_LateUpdateListeners.Count; i < count; i++)
            {
                this.m_LateUpdateListeners[i]?.OnLateUpdate(deltaTime);
            }
        }
    }
}