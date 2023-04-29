using UnityEngine;

namespace LudumDare.Units
{
    [CreateAssetMenu(menuName = "Unit", order = 0)]
    public class DeliveryUnit : ScriptableObject
    {
        [SerializeField]
        private DeliveryType deliveryType;

        public DeliveryType DeliveryType => deliveryType;
        
        [SerializeField]
        private NavigationType navType;

        public NavigationType NavType => navType;
        
        
        [SerializeField]
        private int totalWeight = 1;
        public int TotalWeight => totalWeight;
        
        [SerializeField]
        private int maintenanceCost = 1;
        public int MaintenanceCost => maintenanceCost;
        [SerializeField]
        private int purchaseCost = 1;
        public int PurchaseCost => purchaseCost;
    }
    
    public enum NavigationType
    {
            Grid,
            Air
    }
    public enum DeliveryType
    {
            Mail,
            Package
    }
}