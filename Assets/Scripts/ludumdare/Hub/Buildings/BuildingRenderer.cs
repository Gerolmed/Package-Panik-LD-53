using UnityEngine;
using UnityEngine.UI;

namespace LudumDare.Hub.Buildings
{
    public class BuildingRenderer : MonoBehaviour
    {
        [SerializeField] private Image image;
        public void Render((int level, BuildingLevel buildingLevel) upgrade)
        {
            image.sprite = upgrade.buildingLevel.Sprite;
        }   
    }
}