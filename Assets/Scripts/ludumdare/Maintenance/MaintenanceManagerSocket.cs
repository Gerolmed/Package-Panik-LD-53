using UnityEngine;

namespace LudumDare.Maintenance
{
    [CreateAssetMenu(menuName = "LD/Maintenance Manager Socket", order = 0)]
    public class MaintenanceManagerSocket : ScriptableObject
    {
        public IMaintenanceManager Instance { get; set; }


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