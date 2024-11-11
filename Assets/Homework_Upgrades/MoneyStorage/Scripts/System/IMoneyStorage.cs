using System;

namespace Homework_Upgrades.MoneyStorage.Scripts
{
    public interface IMoneyStorage
    {
        event Action<int> OnMoneyChanged;
        event Action<int> OnMoneyEarned;
        event Action<int> OnMoneySpent;

        void EarnMoney(int amount);
        void SpendMoney(int amount);
        int Money { get; }
        bool CanSpendMoney(int amount);
    }
}