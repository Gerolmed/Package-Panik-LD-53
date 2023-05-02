using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LudumDare.WorldGraph.Warehouses.Impl
{
    public class WarehouseManager: MonoBehaviour, IWarehouseManager
    {
        [SerializeField]
        private WarehouseManagerSocket socketRef;

        private Warehouse[] _warehouses;


        [SerializeField]
        private Camera mainCam;
        public Camera MainCam => mainCam;

        [SerializeField]
        private Tilemap tilemap;
        public Tilemap Tilemap => tilemap;

        [SerializeField]
        private GameObject ghostWarehouse;
        public GameObject GhostWarehouse => ghostWarehouse;

        [SerializeField]
        private Color validGhostColor;
        public Color ValidGhostColor => validGhostColor;

        [SerializeField]
        private Color invalidGhostColor;
        public Color InvalidGhostColor => invalidGhostColor;

        [SerializeField]
        private GameObject[] windowsToCancelMoving;

        private void Awake()
        {
            socketRef.Instance = this;
        }

        private void Start() {
            _warehouses = GetComponentsInChildren<Warehouse>();
        }

        private void Update() {
            if (windowsToCancelMoving.Any(window => window.activeSelf))
                CancelMoving();
        }

        private void CancelMoving()
        {
            _warehouses.ToList().ForEach(w => w.CancelMoving());
        }

        public IEnumerable<Warehouse> GetAll()
        {
            return GetComponentsInChildren<Warehouse>();
        }
    }
}