using UnityEngine;
using System.Collections.Generic;
using LudumDare.Units;

namespace LudumDare.FleetManagement.UI
{
    public class FleetManagerUI : MonoBehaviour {
        private Dictionary<DeliveryUnit, DeliveryUnitUIRenderer> _unitRenderers = new();

        [SerializeField] private GameObject parent;
        

        [SerializeField] private GameObject rendererTemplate;
        [SerializeField] private FleetManagerSocket fleetManagerSocket;


        [SerializeField] private Animator animator;
        private Vector2 _origPos;
        
        private bool _isActive = false;
        public bool IsActive => _isActive;

        private void Awake() {
            _origPos = transform.position;    
        }

        public void AddRenderer(DeliveryUnit unitType)
        {
            var newPanel = Instantiate(rendererTemplate);
            newPanel.transform.SetParent(parent.transform, false);

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

        public void ToggleFleetManager()
        {
            if (!_isActive)
            {
                HideFleetManager();
                gameObject.SetActive(true);
            }
            else
            {
                PlayHideAnimation();
            }

            _isActive = !_isActive;
        }

        public void PlayHideAnimation()
        {
            animator.SetTrigger("Hide");
        }

        public void HideFleetManager()
        {
            transform.position = _origPos;
            gameObject.SetActive(false);
        }
    }
}