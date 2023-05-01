using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using LudumDare.Hub.Buildings;

namespace LudumDare.Analytics 
{
    public class AnalyticsManager : MonoBehaviour, IAnalyticsManager
    {
        [SerializeField] private AnalyticsManagerSocket analyticsManagerSocket;

        [SerializeField] private UnityEvent unlockAnalyticsEvent;
        
        private void Awake()
        {
            analyticsManagerSocket.Instance = this;
        }

        public void UnlockAnalyticsByUpgrade((int level, BuildingLevel buildingLevel, BuildingLevel nextLevel) upgrade)
        {
            if (upgrade.buildingLevel.UnlockAnalytics)            
                unlockAnalyticsEvent.Invoke();
        }
    }
}