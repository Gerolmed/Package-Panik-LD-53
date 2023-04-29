using System;
using UnityEngine;

namespace LudumDare.Units
{
    [RequireComponent(typeof(DeliveryUnitStorage))]
    public class TestAddUnits: MonoBehaviour
    {
        public int count;
        public DeliveryUnit type;


        private void Start()
        {
            for (var i = 0; i < count; i++)
            {
                GetComponent<IDeliveryUnitStorage>().AddUnit(type);
            }
        }
    }
}