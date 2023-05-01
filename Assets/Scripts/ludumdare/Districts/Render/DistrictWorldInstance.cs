using System;
using UnityEngine;

namespace LudumDare.Districts.Render
{
    [ExecuteAlways]
    public class DistrictWorldInstance: MonoBehaviour
    {
        private string ID => gameObject.name;

        private District _district;


        [SerializeField]
        private Vector2Int center;
        
        [SerializeField]
        private Vector2Int size;
        [SerializeField]
        private int price = 1000;
        
        [SerializeField]
        private DistrictRenderer districtRenderer;

        private void Awake()
        {
            _district = new District(ID, GetLowerBounds(), GetUpperBounds(), price);
            GetComponentInParent<DistrictManager>()?.Add(_district);
            UpdateUI();

        }


        private void Start()
        {
            UpdateUI();
        }


        public void PurchaseDistrict()
        {
            GetComponentInParent<DistrictManager>()?.Unlock(_district);
            UpdateUI();
        }


        private void UpdateUI()
        {
            districtRenderer.UpdateDistrict(_district);
        }


        private Vector2Int GetUpperBounds()
        {
            return center + size / 2;
        }


        private Vector2Int GetLowerBounds()
        {
            return center - size / 2;
        }


        public void Unlock(District district)
        {
            if(district != _district) return;
            // DO SMTH
        }


        private void OnValidate()
        {
            _district = new District(ID, GetLowerBounds(), GetUpperBounds(), price);
            UpdateUI();
        }
    }
}