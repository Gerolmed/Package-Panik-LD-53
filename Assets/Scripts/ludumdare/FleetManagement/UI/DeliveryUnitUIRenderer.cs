using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LudumDare.Units;

namespace LudumDare.FleetManagement.UI
{
    public class DeliveryUnitUIRenderer : MonoBehaviour {
        [SerializeField] private Image image;
        [SerializeField] private TMP_Text weightTMP;
        [SerializeField] private Image weightIcon;
        [SerializeField] private TMP_Text maintenanceCostTMP;
        [SerializeField] private TMP_Text purchaseCostTMP; 
        [SerializeField] private TMP_Text amountTMP; 
        [SerializeField] private Button buyButton; 
        [SerializeField] private Button removeButton;

        public Button BuyButton => buyButton; 
        public Button RemoveButton => removeButton;

        [SerializeField] private Sprite letterIcon;
        [SerializeField] private Sprite packageIcon;

        public void Render(DeliveryUnit unitType)
        {
            image.sprite = unitType.Icon;
            image.SetNativeSize();

            weightIcon.sprite = unitType.UnitType == UnitType.MailTruck ? letterIcon : packageIcon;
            weightIcon.SetNativeSize();

            weightTMP.text = unitType.TotalWeight.ToString();
            maintenanceCostTMP.text = unitType.MaintenanceCost.ToString() + " / DAY";
            purchaseCostTMP.text = unitType.PurchaseCost.ToString();
        }

        public void RenderAmount(uint amount)
        {
            amountTMP.text = amount.ToString();
        }
    }
}