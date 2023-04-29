using UnityEngine;

namespace LudumDare.WorldGraph.Warehouses
{
    [CreateAssetMenu(menuName = "LD/Warehouse Manager Socket", order = 0)]
    public class WarehouseManagerSocket: ScriptableObject
    {
        public IWarehouseManager Instance { get; set; }


        private void Awake()
        {
            Instance = null;
        }


        private void OnDestroy()
        {
            Instance = null;
        }
    }
}