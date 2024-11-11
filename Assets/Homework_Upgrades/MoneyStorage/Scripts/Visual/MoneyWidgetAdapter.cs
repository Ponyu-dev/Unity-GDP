using Declarative;
using Homework_Upgrades.MoneyStorage.Scripts;

namespace Game.GamePlay.Upgrades
{
    public sealed class MoneyWidgetAdapter :
        IEnableListener,
        IDisableListener
    {
        private IMoneyStorage _moneyStorage;
        private MoneyWidgetView _moneyWidgetView;

        public void Constructor(IMoneyStorage moneyStorage, MoneyWidgetView moneyWidgetView)
        {
            _moneyStorage = moneyStorage;
            _moneyWidgetView = moneyWidgetView;

            OnMoneyChanged(_moneyStorage.Money);
        }
        
        void IEnableListener.OnEnable()
        {
            _moneyStorage.OnMoneyChanged += OnMoneyChanged;
        }

        private void OnMoneyChanged(int money)
        {
            _moneyWidgetView.SetMoney(money.ToString());
        }

        void IDisableListener.OnDisable()
        {
            _moneyStorage.OnMoneyChanged -= OnMoneyChanged;
        }
    }
}