using UnityEngine;
using LudumDare.Units;

namespace LudumDare.Hub.Buildings
{
    [CreateAssetMenu(menuName = "LD/Building/BuildingLevel")]
    public class BuildingLevel : ScriptableObject
    {
        [SerializeField] private Sprite sprite;
        [SerializeField] private string description;
        [SerializeField] private int price;
        [SerializeField] private RuntimeAnimatorController animatorController = null;
        [SerializeField] private DeliveryUnit unitUnlocked;
        public Sprite Sprite => sprite;
        public string Description => description;
        public int Price => price;
        public RuntimeAnimatorController AnimatorController => animatorController;
        public DeliveryUnit UnitUnlocked => unitUnlocked;
    }
}