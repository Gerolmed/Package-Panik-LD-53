using System.Collections.Generic;
using UnityEngine;

namespace LudumDare.WorldGraph.Warehouses.Impl
{
    public class WarehouseManager: MonoBehaviour, IWarehouseManager
    {
        [SerializeField]
        private WarehouseManagerSocket socketRef;

        private void Awake()
        {
            socketRef.Instance = this;
        }


        public IEnumerable<Warehouse> GetAll()
        {
            return GetComponentsInChildren<Warehouse>();
        }
    }
}