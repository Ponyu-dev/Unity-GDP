using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameManager : MonoBehaviour
    {
        [ShowInInspector, ReadOnly]
        public GameState State { get; private set; }

        [ShowInInspector, ReadOnly]
        private readonly List<IGameListener> m_Listeners = new();

        [ShowInInspector, ReadOnly]
        private readonly List<IGameUpdateListener> m_UpdateListeners = new();
        
        [ShowInInspector, ReadOnly]
        private readonly List<IGameFixedUpdateListener> m_FixedUpdateListeners = new();
        
        [ShowInInspector, ReadOnly]
        private readonly List<IGameLateUpdateListener> m_LateUpdateListeners = new();
        
        private void Update()
        {
            if (this.State != GameState.PLAYING)
            {
                return;
            }

            var deltaTime = Time.deltaTime;
            for (int i = 0, count = this.m_UpdateListeners.Count; i < count; i++)
            {
                var listener = this.m_UpdateListeners[i];
                listener.OnUpdate(deltaTime);
            }
        }

        private void FixedUpdate()
        {
            if (this.State != GameState.PLAYING)
            {
                return;
            }
            
            var deltaTime = Time.fixedDeltaTime;
            for (int i = 0, count = this.m_FixedUpdateListeners.Count; i < count; i++)
            {
                var listener = this.m_FixedUpdateListeners[i];
                listener.OnFixedUpdate(deltaTime);
            }
        }

        private void LateUpdate()
        {
            if (this.State != GameState.PLAYING)
            {
                return;
            }
            
            var deltaTime = Time.deltaTime;
            for (int i = 0, count = this.m_LateUpdateListeners.Count; i < count; i++)
            {
                var listener = this.m_LateUpdateListeners[i];
                listener.OnLateUpdate(deltaTime);
            }
        }
        
        public void AddListener(IGameListener listener)
        {
            if (listener == null)
            {
                return;
            }
            
            this.m_Listeners.Add(listener);

            if (listener is IGameUpdateListener updateListener)
            {
                this.m_UpdateListeners.Add(updateListener);
            }

            if (listener is IGameFixedUpdateListener fixedUpdateListener)
            {
                this.m_FixedUpdateListeners.Add(fixedUpdateListener);
            }

            if (listener is IGameLateUpdateListener lateUpdateListener)
            {
                this.m_LateUpdateListeners.Add(lateUpdateListener);
            }
        }


        public void RemoveListener(IGameListener listener)
        {
            if (listener == null)
            {
                return;
            }
            
            this.m_Listeners.Remove(listener);

            if (listener is IGameUpdateListener updateListener)
            {
                this.m_UpdateListeners.Remove(updateListener);
            }

            if (listener is IGameFixedUpdateListener fixedUpdateListener)
            {
                this.m_FixedUpdateListeners.Remove(fixedUpdateListener);
            }

            if (listener is IGameLateUpdateListener lateUpdateListener)
            {
                this.m_LateUpdateListeners.Remove(lateUpdateListener);
            }
        }

        [Button]
        public void StartGame()
        {
            foreach (var listener in this.m_Listeners)
            {
                if (listener is IGameStartListener startListener)
                {
                    startListener.OnStartGame();
                }
            }

            this.State = GameState.PLAYING;
        }

        [Button]
        public void PauseGame()
        {
            foreach (var listener in this.m_Listeners)
            {
                if (listener is IGamePauseListener pauseListener)
                {
                    pauseListener.OnPauseGame();
                }
            }
            
            this.State = GameState.PAUSED;
        }

        [Button]
        public void ResumeGame()
        {
            foreach (var listener in this.m_Listeners)
            {
                if (listener is IGameResumeListener resumeListener)
                {
                    resumeListener.OnResumeGame();
                }
            }
            
            this.State = GameState.PLAYING;
        }

        [Button]
        public void FinishGame()
        {
            foreach (var listener in this.m_Listeners)
            {
                if (listener is IGameFinishListener finishListener)
                {
                    finishListener.OnFinishGame();
                }
            }
            
            this.State = GameState.FINISHED;
        }
        
        /*public void StopGame()
        {
            Debug.Log("Game over!");
            Time.timeScale = 0;
        }*/
    }
}