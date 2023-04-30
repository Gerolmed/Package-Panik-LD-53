using System.Collections.Generic;
using LudumDare.Units;
using LudumDare.Hub.Buildings;

namespace LudumDare.FleetManagement
{
    public interface IFleetManager
    {
        public Dictionary<DeliveryUnit, uint> UnitTypesAmounts { get; }
        public void UnlockUnitByUpgrade((int level, BuildingLevel buildingLevel, BuildingLevel nextLevel) upgrade);
        public void BuyUnit(DeliveryUnit unitType);
        public void RemoveUnit(DeliveryUnit unitType);

    }
}