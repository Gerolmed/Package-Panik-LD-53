using UnityEngine;
using UnityEngine.UI;

namespace LudumDare.Hub.Buildings
{
    public class BuildingRenderer : MonoBehaviour
    {
        [SerializeField] private Image image;
        public void Render((int level, BuildingLevel buildingLevel, BuildingLevel nextLevel) upgrade)
        {
            image.sprite = upgrade.buildingLevel.Sprite;
        }   
    }
}