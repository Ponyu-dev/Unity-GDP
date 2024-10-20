using VContainer;

namespace SampleGame
{
    public sealed class MenuLoader
    {
        private readonly ISceneLoader _sceneLoader;

        [Inject]
        public MenuLoader(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
        //TODO: Сделать через Addressables
        public void LoadMenu()
        {
            _sceneLoader.LoadNewScene("Menu");
        }
    }
}