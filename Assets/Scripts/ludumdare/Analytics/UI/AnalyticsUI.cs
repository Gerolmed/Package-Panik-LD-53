using UnityEngine;
using UnityEngine.UI;

namespace LudumDare.Analytics.UI
{    
    public class AnalyticsUI : MonoBehaviour {
        [SerializeField] private Button analyticsButton;

        [SerializeField] private Animator animator;
        private Vector2 _origPos;

        private bool _isActive = false;
        public bool IsActive => _isActive;
        
        private void Awake() {
            _origPos = transform.position;    
        }

        public void UnlockAnalyticsButton()
        {
            analyticsButton.interactable = true;
        }      

        public void ToggleAnalytics()
        {
            if (!_isActive)
            {
                HideAnalytics();
                gameObject.SetActive(true);
            }
            else
            {
                PlayHideAnimation();
            }

            _isActive = !_isActive;
        }

        public void PlayHideAnimation()
        {
            animator.SetTrigger("Hide");
        }

        public void HideAnalytics()
        {
            transform.position = _origPos;
            gameObject.SetActive(false);
        } 
    }
}