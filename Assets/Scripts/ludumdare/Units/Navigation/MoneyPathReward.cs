using System;
using LudumDare.MoneySystem;
using LudumDare.Stats;
using UnityEngine;

namespace LudumDare.Units.Navigation
{
    public class MoneyPathReward: MonoBehaviour
    {

        [SerializeField]
        private MoneyManagerSocket moneyManagerSocket;
        [SerializeField]
        private StatTrackManagerManagerSocket statTrack;

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
            statTrack.Instance.MoneyReceived += value;

            if (navigator.Unit.UnitType == UnitType.MailTruck)
            {
                statTrack.Instance.MailDelivered += 1;
            } else 
            if (navigator.Unit.UnitType == UnitType.PackageTruck)
            {
                statTrack.Instance.PackagesDelivered += 1;
            } else 
            if (navigator.Unit.UnitType == UnitType.Drone)
            {
                statTrack.Instance.DronesDelivered += 1;
            }
            
            moneyManagerSocket.Instance.AddMoney(value);
        }
    }
}