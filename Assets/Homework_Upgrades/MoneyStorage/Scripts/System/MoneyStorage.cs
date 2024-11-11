using System;
using UnityEngine;

namespace Homework_Upgrades.MoneyStorage.Scripts
{
    [Serializable]
    public sealed class MoneyStorage : IMoneyStorage
    {
        public event Action<int> OnMoneyChanged;
        public event Action<int> OnMoneyEarned;
        public event Action<int> OnMoneySpent;
        
        [SerializeField] private int _money;
        
        public void EarnMoney(int amount)
        {
            Debug.Log($"EarnMoney {amount}");
            _money += amount;
            OnMoneyEarned?.Invoke(_money);
            OnMoneyChanged?.Invoke(_money);
        }

        public void SpendMoney(int amount)
        {
            _money -= amount;
            OnMoneySpent?.Invoke(_money);
            OnMoneyChanged?.Invoke(_money);
        }

        public int Money => _money;

        public bool CanSpendMoney(int amount)
        {
            var result = amount <= _money;
            Debug.Log($"Can spend {amount} of all money {_money} = {result}");
            return result;
        }
    }
}