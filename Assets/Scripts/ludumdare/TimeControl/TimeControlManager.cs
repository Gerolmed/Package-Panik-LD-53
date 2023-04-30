using System;
using UnityEngine;
using UnityEngine.Events;

namespace LudumDare.TimeControl
{
    public class TimeControlManager: MonoBehaviour, ITimeControlManager
    {
        [SerializeField]
        private TimeControlManagerSocket socketRef;
        
        [Min(1)]
        [SerializeField]
        public float secondsPerHour = 20;
        [Min(1)]
        [SerializeField]
        public float hoursPerCycle = 6;

        [SerializeField]
        public UnityIntEvent onCycle;
        
        public float TimeModifier { private set; get; }

        private TimeMode _timeMode;
        // Time in minutes
        private float _time;
        private int _previousCycle = -1;

        public TimeMode TimeMode
        {
            get => _timeMode;
            set
            {
                _timeMode = value;
                TimeModifier = (float) _timeMode;
            }
        }
        
        private void Awake()
        {
            socketRef.Instance = this;
        }


        private void Start()
        {
            TimeMode = TimeMode.Normal;
        }


        private void Update()
        {
            _time += Time.deltaTime * 60 * TimeModifier / secondsPerHour;
            var currentCycle = Mathf.FloorToInt(HourOfDay / hoursPerCycle);
            if (currentCycle == _previousCycle) return;
            _previousCycle = currentCycle;
            onCycle.Invoke(currentCycle);
        }

        public int MinuteOfHour =>
            Mathf.FloorToInt(_time -
                             (HourOfDay * 60 + DayOfWeek * 60 * 24 + Week * 7 * 60 * 24));
        public int HourOfDay => Mathf.FloorToInt(
            (_time - (DayOfWeek * 60 * 24 + Week * 7 * 60 * 24)) / 60f);
        public int DayOfWeek => Mathf.FloorToInt((_time - Week * 7 * 60 * 24) / (60 * 24));
        public int Week => Mathf.FloorToInt(_time / (7 * 60 * 24));
    }
    
    [Serializable]
    public class UnityIntEvent: UnityEvent<int> {}
}