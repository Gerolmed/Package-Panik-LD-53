using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using LudumDare.Units;
using LudumDare.MoneySystem;
using LudumDare.Hub.Buildings;

namespace LudumDare.FleetManagement 
{
    public class FleetManager : MonoBehaviour, IFleetManager
    {
        [SerializeField] private FleetManagerSocket fleetManagerSocket;
        [SerializeField] private MoneyManagerSocket moneyManagerSocket;
        [SerializeField] private DeliveryUnitStorageSocket deliveryUnitStorageSocket;

        [SerializeField] private UnlockUnitEvent unlockUnitEvent;
        [SerializeField] private UpdateUnitAmountEvent updateAmountEvent;

        public Dictionary<DeliveryUnit, uint> UnitTypesAmounts { get; private set; } = new();
        
        private void Awake()
        {
            fleetManagerSocket.Instance = this;
        }

        public void UnlockUnitByUpgrade((int level, BuildingLevel buildingLevel, BuildingLevel nextLevel) upgrade)
        {
            if (upgrade.buildingLevel.UnitUnlocked == null)
                return;
            
            UnitTypesAmounts.Add(upgrade.buildingLevel.UnitUnlocked, 0);
            unlockUnitEvent.Invoke(upgrade.buildingLevel.UnitUnlocked);
        }

        public void BuyUnit(DeliveryUnit unitType)
        {
            if (moneyManagerSocket.Instance.TryDeduct(unitType.PurchaseCost, TransactionType.UnitAcquisition))
            {
                UnitTypesAmounts[unitType] += 1;
                updateAmountEvent.Invoke(unitType, UnitTypesAmounts[unitType]);

                deliveryUnitStorageSocket.Instance.AddUnit(unitType);
            }
        }

        public void RemoveUnit(DeliveryUnit unitType)
        {
            if (UnitTypesAmounts[unitType] == 0)
                return;

            UnitTypesAmounts[unitType] -= 1;
            updateAmountEvent.Invoke(unitType, UnitTypesAmounts[unitType]);

            deliveryUnitStorageSocket.Instance.RemoveUnit(unitType);
        }
    }

    [System.Serializable]
    public class UnlockUnitEvent: UnityEvent<DeliveryUnit> {}

    [System.Serializable]
    public class UpdateUnitAmountEvent: UnityEvent<DeliveryUnit, uint> {}
}