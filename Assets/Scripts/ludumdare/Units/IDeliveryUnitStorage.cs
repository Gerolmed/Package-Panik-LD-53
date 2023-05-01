using System.Collections.Generic;

namespace LudumDare.Units
{
    public interface IDeliveryUnitStorage
    {
        IEnumerable<IUnitInstance> GetUnits();
        IEnumerable<IUnitInstance> GetAvailableUnits();


        void AddUnit(DeliveryUnit unit);
        void RemoveUnit(DeliveryUnit unit);
    }
}