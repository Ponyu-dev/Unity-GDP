namespace SampleGame
{
    public sealed class MenuLoader
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IAddressablesService _addressablesService;
        
        public MenuLoader(ISceneLoader sceneLoader, IAddressablesService addressablesService)
        {
            _sceneLoader = sceneLoader;
            _addressablesService = addressablesService;
        }
        
        //TODO: Сделать через Addressables
        public void LoadMenu()
        {
            _addressablesService.Clear();
            _sceneLoader.LoadNewScene("Menu");
        }
    }
}