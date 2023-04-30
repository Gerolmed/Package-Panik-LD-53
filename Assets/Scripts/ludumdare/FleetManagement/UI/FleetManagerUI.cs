using UnityEngine;
using System.Collections.Generic;
using LudumDare.Units;

namespace LudumDare.FleetManagement.UI
{
    public class FleetManagerUI : MonoBehaviour {
        private Dictionary<DeliveryUnit, DeliveryUnitUIRenderer> _unitRenderers = new();
        private float _lastY = 0f;

        [SerializeField] private GameObject rendererTemplate;
        [SerializeField] private FleetManagerSocket fleetManagerSocket;

        public void AddRenderer(DeliveryUnit unitType)
        {
            var newPanel = Instantiate(rendererTemplate, new Vector3(0, _lastY), Quaternion.identity);
            newPanel.transform.SetParent(transform, false);

            _lastY -= newPanel.GetComponent<RectTransform>().rect.height;

            var newRenderer = newPanel.GetComponent<DeliveryUnitUIRenderer>();

            newRenderer.BuyButton.onClick.AddListener(() => fleetManagerSocket.Instance.BuyUnit(unitType));
            newRenderer.RemoveButton.onClick.AddListener(() => fleetManagerSocket.Instance.RemoveUnit(unitType));

            _unitRenderers.Add(unitType, newRenderer);
            _unitRenderers[unitType].Render(unitType);
        }

        public void RenderAmount(DeliveryUnit unitType, uint amount)
        {
            _unitRenderers[unitType].RenderAmount(amount);
        }
    }
}