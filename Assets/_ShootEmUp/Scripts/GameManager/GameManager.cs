using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace ShootEmUp
{
    public interface IGameManager
    {
        public void StartTimer();
        public void StartGame();
        public void PauseGame();
        public void ResumeGame();
        public void FinishGame();
        public void RestartGame();
    }
    
    public sealed class GameManager : MonoBehaviour, IGameManager
    {
        [ShowInInspector, ReadOnly]
        private readonly List<IGameUpdateListener> m_UpdateListeners = new();
        
        [ShowInInspector, ReadOnly]
        private readonly List<IGameFixedUpdateListener> m_FixedUpdateListeners = new();
        
        [ShowInInspector, ReadOnly]
        private readonly List<IGameLateUpdateListener> m_LateUpdateListeners = new();
        
        
        [ShowInInspector, ReadOnly]
        public GameState state { get; private set; }

        private readonly GameManagerContext m_Context = new();

        private IObjectResolver m_ObjectResolver;
        
        [Inject]
        private void Construct(IObjectResolver objectResolver)
        {
            m_ObjectResolver = objectResolver;
        }

        private void Awake()
        {
            m_ObjectResolver.Inject(m_Context);
        }
        
        private void Start()
        {
            m_Context.OnInitialize();
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

        [Button]
        public void StartTimer()
        {
            if (state is not (GameState.NONE or GameState.FINISHED)) return;
            
            this.state = GameState.START_TIMER;
            m_Context.OnStartTimer();
        }

        [Button]
        public void StartGame()
        {
            Debug.Log($"StartGame {state}");
            if (state is not (GameState.START_TIMER)) return;
            
            ResumeTime();

            this.state = GameState.PLAYING;
            m_Context.OnStartGame();
        }

        [Button]
        public void PauseGame()
        {
            if (state != GameState.PLAYING) return;

            PauseTime();
            
            this.state = GameState.PAUSED;
            m_Context.OnPauseGame();
        }

        [Button]
        public void ResumeGame()
        {
            if (state != GameState.PAUSED) return;
            
            ResumeTime();
            
            this.state = GameState.PLAYING;
            m_Context.OnResumeGame();
        }

        [Button]
        public void FinishGame()
        {
            if (state != GameState.PLAYING) return;
            
            PauseTime();
            
            this.state = GameState.FINISHED;
            m_Context.OnFinishGame();
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