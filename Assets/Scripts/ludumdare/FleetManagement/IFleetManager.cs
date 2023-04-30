using System.Collections.Generic;
using LudumDare.Units;

namespace LudumDare.FleetManagement
{
    public interface IFleetManager
    {
        public List<DeliveryUnit> UnlockedUnitTypes { get; }
        public void UnlockUnit(DeliveryUnit unitToUnlock);
    }
}