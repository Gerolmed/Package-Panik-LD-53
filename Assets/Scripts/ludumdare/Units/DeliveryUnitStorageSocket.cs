using System;
using UnityEngine;

namespace LudumDare.Units
{
    [CreateAssetMenu(menuName = "LD/Unit/Store Socket", order = 0)]
    public class DeliveryUnitStorageSocket : ScriptableObject
    {
        public IDeliveryUnitStorage Instance { get; set; }


        private void Awake()
        {
            Instance = null;
        }


        private void OnDestroy()
        {
            Instance = null;
        }
    }
}