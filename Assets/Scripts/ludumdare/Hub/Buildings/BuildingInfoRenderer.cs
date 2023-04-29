using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace LudumDare.Hub.Buildings
{
    public class BuildingInfoRenderer : MonoBehaviour
    {
        [SerializeField] private TMP_Text description;
        [SerializeField] private int price;
        public void Render((int level, BuildingLevel buildingLevel, BuildingLevel nextLevel) upgrade)
        {
            description.text = upgrade.nextLevel.Description;
            price = upgrade.nextLevel.Price;
        }   
    }
}