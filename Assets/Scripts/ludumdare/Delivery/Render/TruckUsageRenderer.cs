using System.Collections.Generic;
using LudumDare.Units;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LudumDare.Delivery.Render
{
    public class TruckUsageRenderer: UIBehaviour
    {
        [SerializeField]
        private TMP_Text mailText;
        [SerializeField]
        private TMP_Text packageText;
        [SerializeField]
        private TMP_Text droneText;


        [SerializeField]
        private DeliveryUnitStorageSocket storageSocket;

        [SerializeField]
        private bool showOnlyTotal = false;

        private void Update()
        {
            var total = new Dictionary<UnitType, int>();
            var free = new Dictionary<UnitType, int>();
            
            total.Add(UnitType.MailTruck,0);
            total.Add(UnitType.PackageTruck,0);
            total.Add(UnitType.Drone,0);
            
            free.Add(UnitType.MailTruck,0);
            free.Add(UnitType.PackageTruck,0);
            free.Add(UnitType.Drone,0);
            
            foreach (var unitInstance in storageSocket.Instance.GetUnits())
            {
                total[unitInstance.Type.UnitType] += 1;
                if(unitInstance.Occupied) continue;
                free[unitInstance.Type.UnitType] += 1;
            }

            if (!showOnlyTotal)
            {
                mailText.text = $"{free[UnitType.MailTruck]:D2}/{total[UnitType.MailTruck]:D2}";
                packageText.text = $"{free[UnitType.PackageTruck]:D2}/{total[UnitType.PackageTruck]:D2}";
                droneText.text = $"{free[UnitType.Drone]:D2}/{total[UnitType.Drone]:D2}";
            }
            else
            {
                mailText.text = $"{total[UnitType.MailTruck]:D2}";
                packageText.text = $"{total[UnitType.PackageTruck]:D2}";
                droneText.text = $"{total[UnitType.Drone]:D2}";
            }
        }
    }
}