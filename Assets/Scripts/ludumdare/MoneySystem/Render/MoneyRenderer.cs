using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LudumDare.MoneySystem.Render
{
    public class MoneyRenderer: UIBehaviour
    {
        [SerializeField]
        private TMP_Text moneyDisplay;

        [SerializeField]
        private MoneyManagerSocket moneyManagerSocket;


        private void Update()
        {
            moneyDisplay.text = Formatted(moneyManagerSocket.Instance.Balance);
        }


        private string Formatted(int balance)
        {
            return $"{balance:n0}";
        }
    }
}