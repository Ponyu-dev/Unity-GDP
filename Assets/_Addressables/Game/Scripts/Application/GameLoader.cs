using Zenject;

namespace SampleGame
{
    public sealed class GameLoader
    {
        private readonly ISceneLoader _sceneLoader;

        [Inject]
        public GameLoader(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
        //TODO: Сделать через Addressables
        public void LoadGame()
        {
            _sceneLoader.LoadNewScene("Game");
        }
    }
}