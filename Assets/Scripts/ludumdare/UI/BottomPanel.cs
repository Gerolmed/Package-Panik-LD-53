using UnityEngine;
using UnityEngine.UI;
using LudumDare.Analytics.UI;
using LudumDare.FleetManagement.UI;

namespace LudumDare.UI
{    
    public class BottomPanel : MonoBehaviour {
        [SerializeField] private Button hubButton;       
        [SerializeField] private Button mapButton;

        [SerializeField] private AnalyticsUI analyticsUI;
        [SerializeField] private FleetManagerUI fleetManagerUI;

        [SerializeField] private Animator barAnimator;
        public void ChangeMiddleButton()
        {
            hubButton.gameObject.SetActive(!hubButton.gameObject.activeSelf);
            mapButton.gameObject.SetActive(!mapButton.gameObject.activeSelf);
        }  

        public void CheckHideAnalyticsUI(bool revertMiddleButton)
        {
            if (analyticsUI.IsActive)
            {
                analyticsUI.ToggleAnalytics();

                if (revertMiddleButton)
                    ChangeMiddleButton();
            }
        }

        public void CheckHideFleetManagementUI(bool affectMiddleButton)
        {
            if (fleetManagerUI.IsActive)
            {
                fleetManagerUI.ToggleFleetManager();

                if (affectMiddleButton)
                    ChangeMiddleButton();
            }
        }

        public void ShowBar()
        {
            barAnimator.SetBool("Hide", false);
        }

        public void BarAnimationOnSideButtonPress()
        {
            if (!analyticsUI.IsActive && !fleetManagerUI.IsActive)
                barAnimator.SetBool("Hide", false);
            else
                barAnimator.SetBool("Hide", true);
        }
    }
}