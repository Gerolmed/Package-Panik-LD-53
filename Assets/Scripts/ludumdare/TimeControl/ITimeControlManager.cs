using UnityEngine;

namespace LudumDare.TimeControl
{
    public interface ITimeControlManager
    {
        public float TimeModifier { get; }
        public TimeMode TimeMode { set; get; }
        public bool IsPaused => TimeMode == TimeMode.Paused;

        public int MinuteOfHour { get; }
        public int HourOfDay { get; }
        public int DayOfWeek { get; }
        public int Week { get; }
    }

    public enum TimeMode
    {
        Paused,
        Normal,
        Fast
    }
}