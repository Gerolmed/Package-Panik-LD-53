using UnityEngine;

namespace LudumDare.Districts
{
    [CreateAssetMenu(menuName = "LD/District Manager Socket", order = 0)]
    public class DistrictManagerSocket : ScriptableObject
    {
        public IDistrictManager Instance { get; set; }


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