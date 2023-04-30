using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LudumDare.Units;

namespace LudumDare.FleetManagement.UI
{
    public class DeliveryUnitUIRenderer : MonoBehaviour {
        [SerializeField] private Image image;
        [SerializeField] private TMP_Text nameTMP;
        [SerializeField] private TMP_Text weightTMP;
        [SerializeField] private TMP_Text maintenanceCostTMP;
        [SerializeField] private TMP_Text purchaseCostTMP; 
        [SerializeField] private TMP_Text amountTMP; 

        public void Render(DeliveryUnit deliveryUnit)
        {
            // image.sprite = deliveryUnit.Sprite;
            // nameTMP.text = deliveryUnit.Name;
            weightTMP.text = deliveryUnit.TotalWeight.ToString() + " / day";
            maintenanceCostTMP.text = deliveryUnit.MaintenanceCost.ToString() + " / day";
            purchaseCostTMP.text = deliveryUnit.PurchaseCost.ToString();
            // amountTMP.text = "0";
        }
    }
}