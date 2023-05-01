using System;
using System.Collections;
using LudumDare.MoneySystem;
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


        [SerializeField]
        private GameObject purchaseButton;

        [SerializeField]
        private MoneyManagerSocket moneyManagerSocket;

        private int _price;
        private void Update()
        {
            purchaseButton.SetActive(moneyManagerSocket.Instance.Balance >= _price);
        }


        public void UpdateDistrict(District district)
        {
            _price = district.Price;
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
            if (balance == 0) return "FREE";
            return $"{balance:n0}";
        }
    }
}