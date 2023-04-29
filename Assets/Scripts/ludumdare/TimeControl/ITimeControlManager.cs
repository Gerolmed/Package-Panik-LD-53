namespace LudumDare.TimeControl
{
    public interface ITimeControlManager
    {
        public float TimeModifier { get; }
        public TimeMode TimeMode { set; get; }
        public bool IsPaused => TimeMode == TimeMode.Paused;
    }

    public enum TimeMode
    {
        Paused,
        Normal,
        Fast
    }
}