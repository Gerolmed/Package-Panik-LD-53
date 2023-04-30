using UnityEngine;
using LudumDare.Units;

namespace LudumDare.Hub.Buildings
{
    [CreateAssetMenu(menuName = "LD/Building/BuildingLevel")]
    public class BuildingLevel : ScriptableObject
    {
        [SerializeField] private Sprite sprite;
        [SerializeField] private Sprite hollowSprite;
        [SerializeField] private int price;
        [SerializeField] private int weight;
        [SerializeField] private RuntimeAnimatorController animatorController = null;
        [SerializeField] private RuntimeAnimatorController hollowAnimatorController = null;
        [SerializeField] private DeliveryUnit unitUnlocked;
        public Sprite Sprite => sprite;
        public Sprite HollowSprite => hollowSprite;
        public int Price => price;
        public int Weight => weight;
        public RuntimeAnimatorController AnimatorController => animatorController;
        public RuntimeAnimatorController HollowAnimatorController => hollowAnimatorController;
        public DeliveryUnit UnitUnlocked => unitUnlocked;
    }
}