using System.Collections.Generic;

namespace LudumDare.MoneySystem
{
    public interface IMoneyManager
    {
        public int Balance { get; }
        public IEnumerable<(TransactionType, int)> CollectHistory();
        public void ClearHistory();

        public void AddMoney(int value, TransactionType transactionType = default);
        public bool TryDeduct(int value, TransactionType transactionType = default);
        public void Deduct(int value, TransactionType transactionType = default);
    }
}