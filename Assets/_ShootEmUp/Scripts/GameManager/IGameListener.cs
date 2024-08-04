namespace ShootEmUp
{
    public interface IGameListener { }

    public interface IInitializable : IGameListener
    {
        void Initialize();
    }
    
    public interface IGameTimerListener : IGameListener
    {
        void OnStartTimer();
    }
    
    public interface IGameStartListener : IGameListener
    {
        void OnStartGame();
    }

    public interface IGameFinishListener : IGameListener
    {
        void OnFinishGame();
    }

    public interface IGamePauseListener : IGameListener
    {
        void OnPauseGame();
    }

    public interface IGameResumeListener : IGameListener
    {
        void OnResumeGame();
    }

    public interface IGameUpdateListener : IGameListener
    {
        void OnUpdate(float deltaTime);
    }

    public interface IGameFixedUpdateListener : IGameListener
    {
        void OnFixedUpdate(float deltaTime);
    }

    public interface IGameLateUpdateListener : IGameListener
    {
        void OnLateUpdate(float deltaTime);
    }
}