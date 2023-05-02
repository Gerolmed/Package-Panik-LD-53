namespace LudumDare.Stats
{
    public interface IStatTrackManager
    {
        public int MoneyReceived { get; set; }
        public int MailDelivered { get; set; }
        public int PackagesDelivered { get; set; }
        public int DronesDelivered { get; set; }
    }
}