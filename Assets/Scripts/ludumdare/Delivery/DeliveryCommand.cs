using UnityEngine;
using LudumDare.Units;

namespace LudumDare.Delivery {

    public class DeliveryCommand {
        
        public DeliveryType DeliveryType { get; }

        public Vector2Int Pos { get; }


        public DeliveryCommand(Vector2Int pos, DeliveryType type) {
            DeliveryType = type;
            Pos = pos;
        }

    }

}