using UnityEngine;

namespace LudumDare.TimeControl
{
    [CreateAssetMenu(menuName = "LD/Time Control Socket", order = 0)]
    public class TimeControlManagerSocket : ScriptableObject
    {
        public ITimeControlManager Instance { get; set; }


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