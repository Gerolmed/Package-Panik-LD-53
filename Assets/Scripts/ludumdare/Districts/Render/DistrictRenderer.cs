using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LudumDare.Districts.Render
{
    public class DistrictRenderer: MonoBehaviour
    {
        [SerializeField]
        private GameObject uiRoot;
        [SerializeField]
        private GameObject renderRoot;
        [SerializeField]
        private TMP_Text priceTag;
        [SerializeField]
        private GameObject fillSprite;
        [SerializeField]
        private HorizontalLayoutGroup layoutGroup;

        [SerializeField]
        private DistrictManagerSocket socket;
        public void UpdateDistrict(District district)
        {
            if (Application.isPlaying && !(socket.Instance?.IsLocked(district) ?? true))
            {
                renderRoot.SetActive(false);
            }

            Vector3 lower = (Vector2) district.LowerBound;
            Vector3 upper = (Vector2) district.UpperBound;
            var diagonal = (upper - lower);
            var center = diagonal * .5f + lower;
            uiRoot.transform.position = center;

            fillSprite.transform.position = center;
            fillSprite.transform.localScale = new Vector3(diagonal.x, diagonal.y);

            priceTag.text = Formatted(district.Price);
            
            if(!Application.isPlaying) return;
            LayoutRebuilder.ForceRebuildLayoutImmediate(layoutGroup.GetComponent<RectTransform>());

        }
        
        private string Formatted(int balance)
        {
            return $"{balance:n0}";
        }
    }
}