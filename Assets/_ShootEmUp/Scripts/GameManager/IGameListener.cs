namespace ShootEmUp
{
    public interface IGameListener { }

    public interface IInitGameListener : IGameListener
    {
        void Initialize();
    }
    
    public interface ITimerGameListener : IGameListener
    {
        void OnStartTimer();
    }
    
    public interface IStartGameListener : IGameListener
    {
        void OnStartGame();
    }

    public interface IFinishGameListener : IGameListener
    {
        void OnFinishGame();
    }

    public interface IPauseGameListener : IGameListener
    {
        void OnPauseGame();
    }

    public interface IResumeGameListener : IGameListener
    {
        void OnResumeGame();
    }

    public interface IUpdateGameListener : IGameListener
    {
        void OnUpdate(float deltaTime);
    }

    public interface IFixedUpdateGameListener : IGameListener
    {
        void OnFixedUpdate(float deltaTime);
    }

    public interface ILateUpdateGameListener : IGameListener
    {
        void OnLateUpdate(float deltaTime);
    }
}