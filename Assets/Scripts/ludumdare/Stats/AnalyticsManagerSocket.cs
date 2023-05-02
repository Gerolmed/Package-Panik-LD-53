using UnityEngine;

namespace LudumDare.Stats
{
    [CreateAssetMenu(menuName = "LD/Stat Track Manager Socket", order = 0)]
    public class StatTrackManagerManagerSocket : ScriptableObject
    {
        public IStatTrackManager Instance { get; set; }


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