using System;
using LudumDare.MoneySystem;
using UnityEngine;

namespace LudumDare.Units.Navigation
{
    public class MoneyPathReward: MonoBehaviour
    {

        [SerializeField]
        private MoneyManagerSocket moneyManagerSocket;

        [SerializeField]
        private int mailMoney = 20;
        [SerializeField]
        private int packageMoney = 20;
        [SerializeField]
        private int droneMoney = 20;
        [SerializeField]
        private UnitNavigator navigator;
        
        public void OnNode(PathNode node)
        {
            if(node.Type != PathType.Target) return;

            var value = navigator.Unit.UnitType switch
            {
                UnitType.MailTruck => mailMoney,
                UnitType.PackageTruck => packageMoney,
                UnitType.Drone => droneMoney,
                _ => 0
            };

            moneyManagerSocket.Instance.AddMoney(value);
        }
    }
}