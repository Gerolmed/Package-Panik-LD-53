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
        public float IngameTime { get; }
        public long Cycle { get; }
        int CyclesPerDay { get; }
    }

    public enum TimeMode
    {
        Paused = 0,
        Normal = 1,
        Fast = 4
    }
}