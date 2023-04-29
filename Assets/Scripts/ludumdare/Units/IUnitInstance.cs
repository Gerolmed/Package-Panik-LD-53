namespace LudumDare.Units
{
    public interface IUnitInstance
    {
        public bool Occupied { get; set; }
        public DeliveryUnit Type { get; }
    }

    public class UnitInstanceImpl : IUnitInstance
    {
        public UnitInstanceImpl(DeliveryUnit type)
        {
            Type = type;
        }


        public bool Occupied { set; get; }
        public DeliveryUnit Type { get; }
    }
}