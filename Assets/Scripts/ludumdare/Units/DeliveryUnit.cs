using UnityEngine;

namespace LudumDare.Units
{
    [CreateAssetMenu(menuName = "LD/Unit/Unit Type", order = 0)]
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

        [SerializeField]
        private Sprite facingUp;
        
        [SerializeField]
        private Sprite facingRight;
        
        [SerializeField]
        private Sprite facingDown;
        
        [SerializeField]
        private Sprite facingLeft;

        public Sprite FacingLeft => facingLeft;
        public Sprite FacingUp => facingUp;
        public Sprite FacingRight => facingRight;
        public Sprite FacingDown => facingDown;
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