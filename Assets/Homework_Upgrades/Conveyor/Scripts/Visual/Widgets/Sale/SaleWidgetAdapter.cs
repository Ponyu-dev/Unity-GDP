using Declarative;
using Homework_Upgrades.Conveyor.Scripts.App;
using Homework_Upgrades.Conveyor.Scripts.Entity.Updaters;
using Homework_Upgrades.MoneyStorage.Scripts;
using Core = Homework_Upgrades.Conveyor.Scripts.Entity.Core;

namespace Homework_Upgrades.Conveyor.Scripts.Visual.Widgets.Sale
{
    public sealed class SaleWidgetAdapter :
        IEnableListener,
        IDisableListener
    {
        private ConveyorStorageComponent _coreConveyorUnloadStorageComponent;
        private IMoneyStorage _coreMoneyStorage;
        private SaleWidget _saleWidget;
        private SaleData _saleData;
        
        public void Construct(
            Core core,
            SaleWidget saleWidget)
        {
            _coreConveyorUnloadStorageComponent = core.conveyorUnloadStorageComponent;
            _coreMoneyStorage = core.moneyStorageModel.core.moneyStorage;
            _saleData = core.saleData;
            _saleWidget = saleWidget;

            _saleWidget.SetTxtLumber($"x{_saleData.sellingLumberAmount}");
            _saleWidget.SetTxtCoin($"{_saleData.buyingCoinCurrency}x");
            OnValueChanged(_coreConveyorUnloadStorageComponent.Storage.Current);
        }
        
        void IEnableListener.OnEnable()
        {
            _saleWidget.BtnSale.onClick.AddListener(OnClickSale);
            _coreConveyorUnloadStorageComponent.Storage.OnValueChanged += OnValueChanged;
        }

        private void OnValueChanged(int current)
        {
            _saleWidget.BtnSale.interactable = current > 0;
        }

        private void OnClickSale()
        {
            if (_coreConveyorUnloadStorageComponent.Storage.Current <= 0)
                return;
            
            _coreConveyorUnloadStorageComponent.SaleProduct(_saleData.sellingLumberAmount);
            _coreMoneyStorage.EarnMoney(_saleData.buyingCoinCurrency);
        }

        void IDisableListener.OnDisable()
        {
            _coreConveyorUnloadStorageComponent.Storage.OnValueChanged -= OnValueChanged;
            _saleWidget.BtnSale.onClick.RemoveListener(OnClickSale);
        }
    }
}