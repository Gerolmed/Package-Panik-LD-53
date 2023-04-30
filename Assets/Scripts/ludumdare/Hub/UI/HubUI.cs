using UnityEngine;    

namespace LudumDare.Hub.UI
{
    public class HubUI : MonoBehaviour {
        [SerializeField] private Animator animator;
        [SerializeField] private GameObject background;
        [SerializeField] private GameObject foreground;
        private Vector2 _backgroundOrigPos;
        private Vector2 _foregroundOrigPos;
        
        private void Awake() {
            _backgroundOrigPos = background.transform.position;    
            _foregroundOrigPos = foreground.transform.position;    
        }
        
        public void ShowHub()
        {
            gameObject.SetActive(true);
        }

        public void PlayHideAnimation()
        {
            animator.SetTrigger("Hide");
        }

        public void HideHub()
        {
            background.transform.position = _backgroundOrigPos;
            foreground.transform.position = _foregroundOrigPos;
            gameObject.SetActive(false);
        }
    }
}