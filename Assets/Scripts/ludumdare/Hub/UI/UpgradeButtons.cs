using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LudumDare.Hub.UI
{    
    public class UpgradeButtons : MonoBehaviour {
        [SerializeField] private UpgradeButtonContainer[] upgradeButtonContainers;
        private void Start() {
            for (int i = 0; i < upgradeButtonContainers.Length; i++)
            {
                int idx = i;
                upgradeButtonContainers[idx].UpgradeButton.onClick.AddListener(() => ShowUpgradeButtons(upgradeButtonContainers[idx]));
                upgradeButtonContainers[idx].UpgradeButton.onClick.AddListener(() => HideSecondaryButtons(upgradeButtonContainers[idx]));
                upgradeButtonContainers[idx].ConfirmButton.onClick.AddListener(() => ShowUpgradeButton(upgradeButtonContainers[idx]));
                upgradeButtonContainers[idx].CancelButton.onClick.AddListener(() => ShowUpgradeButton(upgradeButtonContainers[idx]));
            }
        }

        private void ShowUpgradeButtons(UpgradeButtonContainer ignoreContainer)
        {
            for (int i = 0; i < upgradeButtonContainers.Length; i++)
            {
                if (upgradeButtonContainers[i].UpgradeButton != ignoreContainer.UpgradeButton) 
                {
                    upgradeButtonContainers[i].UpgradeButton.GetComponent<Image>().enabled = true;  
                    upgradeButtonContainers[i].UpgradeButton.GetComponentInChildren<TMP_Text>().enabled = true;
                }
            }
        }

        private void HideSecondaryButtons(UpgradeButtonContainer ignoreContainer)
        {
            for (int i = 0; i < upgradeButtonContainers.Length; i++)
            {
                if (upgradeButtonContainers[i].UpgradeButton != ignoreContainer.UpgradeButton) {
                    upgradeButtonContainers[i].ConfirmButton.gameObject.SetActive(false);
                    upgradeButtonContainers[i].CancelButton.gameObject.SetActive(false);
                }
            }
        }

        private void ShowUpgradeButton(UpgradeButtonContainer container)
        {
            container.UpgradeButton.GetComponent<Image>().enabled = true;  
            container.UpgradeButton.gameObject.GetComponentInChildren<TMP_Text>().enabled = true;
        }
    }

    [System.Serializable]
    public struct UpgradeButtonContainer
    {
        public Button UpgradeButton;
        public Button ConfirmButton;
        public Button CancelButton;
    }
}