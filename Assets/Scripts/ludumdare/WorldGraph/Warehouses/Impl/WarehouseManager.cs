using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LudumDare.WorldGraph.Warehouses.Impl
{
    public class WarehouseManager: MonoBehaviour, IWarehouseManager
    {
        [SerializeField]
        private WarehouseManagerSocket socketRef;

        [SerializeField]
        private Camera mainCam;
        public Camera MainCam => mainCam;

        [SerializeField]
        private Tilemap tilemap;
        public Tilemap Tilemap => tilemap;

        [SerializeField]
        private GameObject ghostWarehouse;
        public GameObject GhostWarehouse => ghostWarehouse;

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