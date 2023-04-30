namespace LudumDare.Satisfaction
{
    public interface ISatisfactionManager
    {
        public int Current { get; }
        public int Total { get; }
        public float Percentage { get; }
    }
}