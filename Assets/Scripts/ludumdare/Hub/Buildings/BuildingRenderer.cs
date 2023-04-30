using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace LudumDare.Hub.Buildings
{
    public class BuildingRenderer : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Animator animator = null;


        [Header("Info")]
        [SerializeField] private TMP_Text weightTMP;


        [Header("Upgrade Preview")]
        [SerializeField] private TMP_Text previewWeightTMP;
        [SerializeField] private TMP_Text upgradeWeightTMP;
        [SerializeField] private TMP_Text upgradeCostTMP;


        [Header("Panels")]

        [SerializeField] private GameObject info;
        [SerializeField] private GameObject upgradePreview;
        

        [Header("Buttons")]
        [SerializeField] private Button upgradeButton;
        [SerializeField] private Button confirmButton;
        [SerializeField] private Button cancelButton;


        private RuntimeAnimatorController _currentAnimController = null;
        private RuntimeAnimatorController _hollowAnimController = null;
        private Sprite _currentSprite = null;
        private Sprite _hollowSprite = null;
        private bool _maxLevel = false;
        private bool _previewingUpgrade = false;

        public void Render((int level, BuildingLevel buildingLevel, BuildingLevel nextLevel) upgrade)
        {
            if (upgrade.nextLevel == null)
                _maxLevel = true;

            _currentSprite = upgrade.buildingLevel.Sprite;
            image.sprite = _currentSprite;
            image.SetNativeSize();

            if (upgrade.buildingLevel.HollowSprite != null)
                _hollowSprite = upgrade.buildingLevel.HollowSprite;

            if (upgrade.buildingLevel.Weight != -1)
            {
                weightTMP.text = upgrade.buildingLevel.Weight.ToString();
                previewWeightTMP.text = upgrade.buildingLevel.Weight.ToString();

                if (!_maxLevel)
                {
                    upgradeWeightTMP.text = upgrade.nextLevel.Weight.ToString();
                }
            }

            if (!_maxLevel)
                upgradeCostTMP.text = upgrade.nextLevel.Price.ToString();
            
            if (animator) 
            {
                _currentAnimController = upgrade.buildingLevel.AnimatorController;
                if (upgrade.buildingLevel.HollowAnimatorController != null)
                    _hollowAnimController = upgrade.buildingLevel.HollowAnimatorController;

                animator.runtimeAnimatorController = _currentAnimController;
            }
        }

        public void ShowInfo()
        {
            if (_previewingUpgrade)
                return;

            if (!_maxLevel)
                upgradeButton.gameObject.SetActive(true);
            
            info.SetActive(true);
        }

        public void ShowUpgradePreview()
        {
            confirmButton.gameObject.SetActive(true);
            cancelButton.gameObject.SetActive(true);
            upgradePreview.SetActive(true);

            if (_hollowAnimController)
                animator.runtimeAnimatorController = _hollowAnimController;
            else if (animator)
                animator.runtimeAnimatorController = null;

            image.sprite = _hollowSprite;
            image.SetNativeSize();

            _previewingUpgrade = true;
        }

        public void HideInfo()
        {
            upgradeButton.gameObject.SetActive(false);
            info.SetActive(false);
        }

        public void HideUpgradePreview()
        {
            confirmButton.gameObject.SetActive(false);
            cancelButton.gameObject.SetActive(false);
            upgradePreview.SetActive(false);

            if (_currentAnimController)
                animator.runtimeAnimatorController = _currentAnimController;
            else if (animator)
                animator.runtimeAnimatorController = null;

            image.sprite = _currentSprite;
            image.SetNativeSize();

            _previewingUpgrade = false;
        }
    }
}