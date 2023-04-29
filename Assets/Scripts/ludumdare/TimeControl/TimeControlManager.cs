using UnityEngine;

namespace LudumDare.TimeControl
{
    public class TimeControlManager: MonoBehaviour, ITimeControlManager
    {
        [SerializeField]
        private TimeControlManagerSocket socketRef;


        private void Awake()
        {
            socketRef.Instance = this;
        }
    }
}