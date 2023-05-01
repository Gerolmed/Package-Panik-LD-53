using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LudumDare.Units
{
    public class DeliveryUnitStorage: MonoBehaviour, IDeliveryUnitStorage
    {
        
        private List<IUnitInstance> _unitInstances = new();

        [SerializeField]
        private DeliveryUnitStorageSocket socketRef;


        private void Awake()
        {
            socketRef.Instance = this;
        }


        public IEnumerable<IUnitInstance> GetUnits()
        {
            return _unitInstances;
        }


        public IEnumerable<IUnitInstance> GetAvailableUnits()
        {
            return GetUnits().Where(unit => !unit.Occupied);
        }


        public void AddUnit(DeliveryUnit unit)
        {
            _unitInstances.Add(new UnitInstanceImpl(unit));
        }


        public void RemoveUnit(DeliveryUnit unit)
        {
            _unitInstances.Remove(_unitInstances.Last(unitInstance => unitInstance.Type == unit));
        }
    }
}