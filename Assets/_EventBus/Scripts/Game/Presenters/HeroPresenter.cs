using _EventBus.Scripts.Entities.Hero;
using UI;
using VContainer;

namespace _EventBus.Scripts.Game.Presenters
{
    public interface IHeroPresenter
    {
        void Initialize(HeroConfig heroConfig, HeroView heroView);
    }
    
    public class HeroPresenter : IHeroPresenter
    {
        private HeroConfig _heroConfig;
        private HeroView _heroView;
        
        [Inject]
        public HeroPresenter() { }
        
        public void Initialize(HeroConfig heroConfig, HeroView heroView)
        {
            _heroConfig = heroConfig;
            _heroView = heroView;
            InitView();
        }

        private void InitView()
        {
            _heroView.SetIcon(_heroConfig.heroPortrait);
            _heroView.SetStats($"{_heroConfig.hero.damage}/{_heroConfig.hero.health}");//атака/здоровье
        }
    }
}