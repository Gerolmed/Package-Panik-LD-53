using LudumDare.WorldGraph.TilemapGraph;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

namespace LudumDare.WorldGraph.Warehouses.Impl
{
    public class Warehouse: MonoBehaviour, IWarehouse
    {
        private WarehouseManager _warehouseManager;
        private bool _ghostFollowMouse = false;
        private Vector2 _currentPos;

        private GameObject _ghostWarehouseInstance = null;
        private bool _ghostTileValid;
        private void Start() {
            _warehouseManager = GetComponentInParent<WarehouseManager>();

            _currentPos = transform.position;
        }
        public Vector2Int GetPosition()
        {
            var pos = transform.position;

            return new Vector2Int(Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y));
        }

        public void PickWarehouse()
        {
            _ghostFollowMouse = true;
            _ghostWarehouseInstance = Instantiate(_warehouseManager.GhostWarehouse);
        }

        private void PutWarehouse()
        {
            if (!_ghostTileValid)
                return;

            transform.position = _ghostWarehouseInstance.transform.position;

            _ghostFollowMouse = false;
            Destroy(_ghostWarehouseInstance);
            _ghostWarehouseInstance = null;

            _ghostTileValid = false;
        }

        private void CancelMoving()
        {
            _ghostFollowMouse = false;
            Destroy(_ghostWarehouseInstance);
            _ghostWarehouseInstance = null;

            _ghostTileValid = false;
        }

        private void Update() {
            if (_ghostFollowMouse)
            {
                GhostFollowMouse();

                if (Input.GetMouseButtonDown(0))
                    PutWarehouse();

                if (Input.GetMouseButtonDown(1))
                    CancelMoving();
            }
        }

        private void GhostFollowMouse()
        {
            Vector2 screenToWorld = _warehouseManager.MainCam.ScreenToWorldPoint(Input.mousePosition);

            Vector3Int tilePosition = _warehouseManager.Tilemap.WorldToCell(screenToWorld);

            TileBase tileBase = _warehouseManager.Tilemap.GetTile(tilePosition);
            if (tileBase is NodeTile nodeTile)
            {
                _ghostTileValid = nodeTile.IsWarehouseLocation; 

                float x = Mathf.Floor(screenToWorld.x) + 0.5f;
                float y = Mathf.Floor(screenToWorld.y) + 0.5f;

                _ghostWarehouseInstance.transform.position = new Vector2(x, y);
            }
        }
    }
}