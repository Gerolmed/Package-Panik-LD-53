using LudumDare.WorldGraph.TilemapGraph;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

namespace LudumDare.WorldGraph.Warehouses.Impl
{
    public class Warehouse: MonoBehaviour, IWarehouse, IPointerClickHandler
    {
        private WarehouseManager _warehouseManager;
        private bool _ghostFollowMouse = false;
        private Vector2 _currentPos;

        private GameObject _ghostWarehouseInstance = null;
        private SpriteRenderer _ghostInstanceRenderer = null;
        private bool _ghostTileValid;

        private bool _isMouseDown = true;
        private float _mouseDownTime = 0f;
        [SerializeField] private float clickTime = 0.2f;
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
            _ghostInstanceRenderer = _ghostWarehouseInstance.GetComponentInChildren<SpriteRenderer>();
        }

        private void PutWarehouse()
        {
            if (!_ghostTileValid || EventSystem.current.IsPointerOverGameObject())
                return;

            if (EventSystem.current.IsPointerOverGameObject())
                return;

            transform.position = _ghostWarehouseInstance.transform.position;

            _ghostFollowMouse = false;
            Destroy(_ghostWarehouseInstance);
            _ghostWarehouseInstance = null;
            _ghostInstanceRenderer = null;

            _ghostTileValid = false;
        }

        public void CancelMoving()
        {
            _ghostFollowMouse = false;
            Destroy(_ghostWarehouseInstance);
            _ghostWarehouseInstance = null;
            _ghostInstanceRenderer = null;

            _ghostTileValid = false;
        }

        private void Update() {
            if (_ghostFollowMouse)
            {
                GhostFollowMouse();

                if (!_ghostTileValid || EventSystem.current.IsPointerOverGameObject())
                    _ghostInstanceRenderer.color = _warehouseManager.InvalidGhostColor;
                else
                    _ghostInstanceRenderer.color = _warehouseManager.ValidGhostColor;

                if (Input.GetMouseButtonDown(0))
                {
                    _isMouseDown = true;
                    _mouseDownTime = Time.time;
                }
                if (Input.GetMouseButtonUp(0) && _isMouseDown)
                {
                    float timeElapsed = Time.time - _mouseDownTime;
                    if (timeElapsed < clickTime)
                    {
                        PutWarehouse();
                    }
                    _isMouseDown = false;
                }

                if (!_isMouseDown && Input.GetMouseButtonDown(1))
                    CancelMoving();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left && !_ghostFollowMouse)
                PickWarehouse();
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