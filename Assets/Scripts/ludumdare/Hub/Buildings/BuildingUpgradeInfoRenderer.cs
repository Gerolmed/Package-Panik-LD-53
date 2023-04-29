using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace LudumDare.Hub.Buildings
{
    public class BuildingUpgradeInfoRenderer : MonoBehaviour
    {
        [SerializeField] private Button upgradeButton;
        [SerializeField] private TMP_Text currentLevelTMP;
        [SerializeField] private TMP_Text nextLevelTMP;
        [SerializeField] private TMP_Text descriptionTMP;
        [SerializeField] private TMP_Text priceTMP;
        public void Render((int level, BuildingLevel buildingLevel, BuildingLevel nextLevel) upgrade)
        {
            if (upgrade.nextLevel == null) {
                upgradeButton.gameObject.SetActive(false);
                gameObject.SetActive(false);
                return;
            }

            currentLevelTMP.text = upgrade.level.ToString();
            nextLevelTMP.text = (upgrade.level + 1).ToString();
            descriptionTMP.text = upgrade.nextLevel.Description;
            priceTMP.text = upgrade.nextLevel.Price.ToString();
        }   
    }
}