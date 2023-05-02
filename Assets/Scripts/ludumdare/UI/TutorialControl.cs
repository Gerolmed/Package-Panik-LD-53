using UnityEngine;

namespace LudumDare.UI
{
    public class TutorialControl: MonoBehaviour
    {
        [SerializeField]
        public GameObject[] pages;
        [SerializeField]
        public GameObject menu;

        private int _index = 0;


        public void Next()
        {
            _index++;

            if (_index >= pages.Length)
            {
                menu.SetActive(false);
                return;
            }
            
            pages[_index-1].SetActive(false);
            pages[_index].SetActive(true);
        }
    }
}