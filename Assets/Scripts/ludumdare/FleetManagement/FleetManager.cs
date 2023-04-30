using System.Collections.Generic;
using LudumDare.Units;
using UnityEngine;

namespace LudumDare.FleetManagement 
{
    public class FleetManager : MonoBehaviour, IFleetManager
    {
        [SerializeField] private FleetManagerSocket fleetManagerSocket;

        public List<DeliveryUnit> UnlockedUnitTypes { get; private set; }
        
        private void Awake()
        {
            fleetManagerSocket.Instance = this;
        }

        public void UnlockUnit(DeliveryUnit unitToUnlock)
        {
            throw new System.NotImplementedException();
        }
    }
}