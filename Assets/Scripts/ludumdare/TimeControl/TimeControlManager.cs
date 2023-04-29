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

            TimeMode = TimeMode.Normal;
        }
        

        public float TimeModifier { private set; get; }

        private TimeMode _timeMode;

        public TimeMode TimeMode
        {
            get => _timeMode;
            set
            {
                _timeMode = value;
                TimeModifier = (float) _timeMode;
            }
        }
    }
}