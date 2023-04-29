using UnityEngine;

namespace LudumDare.Hub.Buildings
{
    [CreateAssetMenu(menuName = "BuildingLevel")]
    public class BuildingLevel : ScriptableObject
    {
        [SerializeField] private Sprite sprite;
        [SerializeField] private string description;
        [SerializeField] private int price;
        [SerializeField] private RuntimeAnimatorController animatorController = null;
        public Sprite Sprite => sprite;
        public string Description => description;
        public int Price => price;
        public RuntimeAnimatorController AnimatorController => animatorController;
    }
}