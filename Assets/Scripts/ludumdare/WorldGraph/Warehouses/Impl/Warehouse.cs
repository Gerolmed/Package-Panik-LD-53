using UnityEngine;

namespace LudumDare.WorldGraph.Warehouses.Impl
{
    public class Warehouse: MonoBehaviour, IWarehouse
    {
        public Vector2Int GetPosition()
        {
            var pos = transform.position;

            return new Vector2Int(Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y));
        }
    }
}