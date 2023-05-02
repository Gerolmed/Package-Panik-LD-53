using System.Linq;
using UnityEngine;
using LudumDare.MoneySystem;
using LudumDare.TimeControl;
using LudumDare.Units;

namespace LudumDare.Maintenance
{
    public class MaintenanceManager : MonoBehaviour, IMaintenanceManager
    {
        [SerializeField]
        private MaintenanceManagerSocket maintenanceManagerSocket;

        [SerializeField]
        private TimeControlManagerSocket timeControlManagerSocket;

        [SerializeField]
        private MoneyManagerSocket moneyManagerSocket;

        [SerializeField]
        private DeliveryUnitStorageSocket deliveryUnitStorageSocket;
        private void Awake()
        {
            maintenanceManagerSocket.Instance = this;
        }

        public void CheckCycle() {
            if (timeControlManagerSocket.Instance.Cycle % timeControlManagerSocket.Instance.CyclesPerDay == 0)
            {
                DeductMoney();
            }
        }

        private void DeductMoney()
        {
            deliveryUnitStorageSocket.Instance.GetUnits()
                .ToList()
                .ForEach(unit => moneyManagerSocket.Instance.Deduct(unit.Type.MaintenanceCost, TransactionType.Maintenance));
        }
    }
}