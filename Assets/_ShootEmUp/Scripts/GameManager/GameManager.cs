using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameManager : MonoBehaviour
    {
        [ShowInInspector, ReadOnly]
        public GameState state { get; private set; }

        [ShowInInspector, ReadOnly]
        private readonly List<IGameListener> m_Listeners = new();

        [ShowInInspector, ReadOnly]
        private readonly List<IGameUpdateListener> m_UpdateListeners = new();
        
        [ShowInInspector, ReadOnly]
        private readonly List<IGameFixedUpdateListener> m_FixedUpdateListeners = new();
        
        [ShowInInspector, ReadOnly]
        private readonly List<IGameLateUpdateListener> m_LateUpdateListeners = new();

        private void Start()
        {
            foreach (var listener in m_Listeners)
            {
                if (listener is IInitializable initializable)
                    initializable.Initialize();
            }
        }

        private void Update()
        {
            if (this.state != GameState.PLAYING)
                return;

            var deltaTime = Time.deltaTime;
            for (int i = 0, count = this.m_UpdateListeners.Count; i < count; i++)
            {
                var listener = this.m_UpdateListeners[i];
                listener.OnUpdate(deltaTime);
            }
        }

        private void FixedUpdate()
        {
            if (this.state != GameState.PLAYING)
                return;
            
            var deltaTime = Time.fixedDeltaTime;
            for (int i = 0, count = this.m_FixedUpdateListeners.Count; i < count; i++)
            {
                var listener = this.m_FixedUpdateListeners[i];
                listener.OnFixedUpdate(deltaTime);
            }
        }

        private void LateUpdate()
        {
            if (this.state != GameState.PLAYING)
                return;
            
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

            switch (listener)
            {
                case IGameUpdateListener updateListener:
                    this.m_UpdateListeners.Add(updateListener);
                    break;
                case IGameFixedUpdateListener fixedUpdateListener:
                    this.m_FixedUpdateListeners.Add(fixedUpdateListener);
                    break;
                case IGameLateUpdateListener lateUpdateListener:
                    this.m_LateUpdateListeners.Add(lateUpdateListener);
                    break;
            }
        }
        
        public void RemoveListener(IGameListener listener)
        {
            if (listener == null)
            {
                return;
            }
            
            this.m_Listeners.Remove(listener);

            switch (listener)
            {
                case IGameUpdateListener updateListener:
                    this.m_UpdateListeners.Remove(updateListener);
                    break;
                case IGameFixedUpdateListener fixedUpdateListener:
                    this.m_FixedUpdateListeners.Remove(fixedUpdateListener);
                    break;
                case IGameLateUpdateListener lateUpdateListener:
                    this.m_LateUpdateListeners.Remove(lateUpdateListener);
                    break;
            }
        }

        [Button]
        public void StartTimer()
        {
            if (state is not (GameState.NONE or GameState.FINISHED)) return;
            
            this.state = GameState.START_TIMER;
            
            foreach (var listener in this.m_Listeners)
            {
                if (listener is IGameTimerListener startTimerListener)
                {
                    startTimerListener.OnStartTimer();
                }
            }
        }

        [Button]
        public void StartGame()
        {
            Debug.Log($"StartGame {state}");
            if (state is not (GameState.START_TIMER)) return;
            
            ResumeTime();
            
            foreach (var listener in this.m_Listeners)
            {
                if (listener is IGameStartListener startListener)
                {
                    startListener.OnStartGame();
                }
            }

            this.state = GameState.PLAYING;
        }

        [Button]
        public void PauseGame()
        {
            if (state != GameState.PLAYING) return;

            PauseTime();
            
            foreach (var listener in this.m_Listeners)
            {
                if (listener is IGamePauseListener pauseListener)
                {
                    pauseListener.OnPauseGame();
                }
            }
            
            this.state = GameState.PAUSED;
        }

        [Button]
        public void ResumeGame()
        {
            if (state != GameState.PAUSED) return;
            
            ResumeTime();
            
            foreach (var listener in this.m_Listeners)
            {
                if (listener is IGameResumeListener resumeListener)
                {
                    resumeListener.OnResumeGame();
                }
            }
            
            this.state = GameState.PLAYING;
        }

        [Button]
        public void FinishGame()
        {
            if (state != GameState.PLAYING) return;
            
            PauseTime();
            
            foreach (var listener in this.m_Listeners)
            {
                if (listener is IGameFinishListener finishListener)
                {
                    finishListener.OnFinishGame();
                }
            }
            
            this.state = GameState.FINISHED;
        }

        public void RestartGame()
        {
            if (state != GameState.FINISHED) return;
            
            StartTimer();
        }

        private void PauseTime()
        {
            Time.timeScale = 0f;
        }
        
        private void ResumeTime()
        {
            Time.timeScale = 1f;
        }
    }
}