using UnityEngine;

namespace LudumDare.FleetManagement
{
    [CreateAssetMenu(menuName = "LD/Fleet Manager Socket", order = 0)]
    public class FleetManagerSocket : ScriptableObject
    {
        public IFleetManager Instance { get; set; }


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