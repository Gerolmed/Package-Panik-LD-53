using System;
using System.Collections.Generic;
using System.Linq;
using LudumDare.Utils;
using UnityEngine;

namespace LudumDare.MoneySystem
{
    public class MoneyManager : MonoBehaviour, IMoneyManager
    {
        [SerializeField]
        private MoneyManagerSocket moneyManagerSocket;

        private readonly Dictionary<TransactionType, int> _history = new();


        public int Balance { get; private set; }


        private void Awake()
        {
            moneyManagerSocket.Instance = this;
        }


        public IEnumerable<(TransactionType, int)> CollectHistory()
        {
            return _history.Select(pair => (pair.Key, pair.Value));
        }


        public void ClearHistory()
        {
            _history.Clear();
        }


        public void AddMoney(int value, TransactionType transactionType = default)
        {
            _history.ComputeOrCreate(transactionType, () => 0, val => val + value);
            Balance += value;
        }


        public bool TryDeduct(int value, TransactionType transactionType = default)
        {
            if (value > Balance) return false;
            _history.ComputeOrCreate(transactionType, () => 0, val => val - value);
            Balance -= value;
            return true;
        }
    }

    public enum TransactionType
    {
        Unknown,
        IncomeMail,
        IncomePackage,
        Maintenance,
        UnitAcquisition,
        Upgrades,
        Loan,
        LoanFee
    }
}