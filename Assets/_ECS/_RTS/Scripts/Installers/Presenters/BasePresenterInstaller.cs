using _ECS._RTS.Scripts.Services;
using _ECS._RTS.Scripts.UI.Presenters;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _ECS._RTS.Scripts.Installers.Presenters
{
    public class BasePresenterInstaller : IStartable
    {
        private readonly BaseInstaller _baseInstaller;
        private readonly IObjectResolver _resolver;
        
        private readonly HealthService _healthService;
        private HealthPresenter _presenter;

        [Inject]
        public BasePresenterInstaller(
            IObjectResolver resolver,
            HealthService healthService,
            BaseInstaller baseInstaller)
        {
            Debug.Log("[BasePresenterInstaller] Constructor()");
            _resolver = resolver;
            _baseInstaller = baseInstaller;
            _healthService = healthService;
        }

        public void Start()
        {
            Debug.Log("[BasePresenterInstaller] Start()");
            
            _healthService.RegisterEntity(_baseInstaller.EntityId, _baseInstaller.GetHealth().Max);
            var reactiveHealth = _healthService.GetHealth(_baseInstaller.EntityId);
            _presenter = _resolver.Resolve<HealthPresenter>();
            _presenter.Init(reactiveHealth, _baseInstaller.GetHealthView());
        }
    }
}