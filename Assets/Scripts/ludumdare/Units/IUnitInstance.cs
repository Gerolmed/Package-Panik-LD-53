namespace LudumDare.Units
{
    public interface IUnitInstance
    {
        public bool Occupied { get; }
        public DeliveryUnit Type { get; }
    }

    public class UnitInstanceImpl : IUnitInstance
    {
        public UnitInstanceImpl(DeliveryUnit type)
        {
            Type = type;
        }


        public bool Occupied { private set; get; }
        public DeliveryUnit Type { get; }
    }
}