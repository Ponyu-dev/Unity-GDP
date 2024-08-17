using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ShootEmUp
{
    public interface IGameManager : ITickable, IFixedTickable, ILateTickable
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

        public void Tick()
        {
            if (this.state != GameState.PLAYING)
                return;

            var deltaTime = Time.deltaTime;
            m_Context.OnTick(deltaTime);
        }

        public void FixedTick()
        {
            if (this.state != GameState.PLAYING)
                return;
            
            var deltaTime = Time.fixedDeltaTime;
            m_Context.OnFixedTick(deltaTime);
        }

        public void LateTick()
        {
            if (this.state != GameState.PLAYING)
                return;
            
            var deltaTime = Time.deltaTime;
            m_Context.OnLateTick(deltaTime);
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