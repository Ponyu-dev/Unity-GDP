using Elementary;
using Game.GamePlay.Upgrades;
using Sirenix.OdinInspector;

namespace Homework_Upgrades.Conveyor.Scripts.Entity.Updaters
{
    public class ConveyorProduceTimeComponent : ConveyorUpdate 
    {
        private float _defaultLoadStorage;
        private float NewMaxLoadStorage => _defaultLoadStorage * (Level -1);

        [ReadOnly, ShowInInspector]
        private FloatVariableLimited _produceTime;
        public FloatVariableLimited ProduceTime => _produceTime;

        public void Constructor(float produceTime, IMoneyStorage moneyStorage)
        {
            //base.Constructor(moneyStorage);
            
            _defaultLoadStorage = produceTime;
            _produceTime = new FloatVariableLimited
            {
                Current = 1,
                MaxValue = NewMaxLoadStorage
            };
        }

        protected override void UpdateMaxLevel()
        {
            _produceTime.MaxValue += NewMaxLoadStorage;
        }
    }
}