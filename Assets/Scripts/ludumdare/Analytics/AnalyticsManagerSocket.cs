using UnityEngine;

namespace LudumDare.Analytics
{
    [CreateAssetMenu(menuName = "LD/Analytics Manager Socket", order = 0)]
    public class AnalyticsManagerSocket : ScriptableObject
    {
        public IAnalyticsManager Instance { get; set; }


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