using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LudumDare.Units;

namespace LudumDare.FleetManagement.UI
{
    public class DeliveryUnitUIRenderer : MonoBehaviour {
        [SerializeField] private Image image;
        [SerializeField] private TMP_Text weightTMP;
        [SerializeField] private TMP_Text maintenanceCostTMP;
        [SerializeField] private TMP_Text purchaseCostTMP; 
        [SerializeField] private TMP_Text amountTMP; 
        [SerializeField] private Button buyButton; 
        [SerializeField] private Button removeButton;

        public Button BuyButton => buyButton; 
        public Button RemoveButton => removeButton; 

        public void Render(DeliveryUnit unitType)
        {
            image.sprite = unitType.Icon;
            image.SetNativeSize();

            weightTMP.text = unitType.TotalWeight.ToString();
            maintenanceCostTMP.text = unitType.MaintenanceCost.ToString();
            purchaseCostTMP.text = unitType.PurchaseCost.ToString();
        }

        public void RenderAmount(uint amount)
        {
            amountTMP.text = amount.ToString();
        }
    }
}