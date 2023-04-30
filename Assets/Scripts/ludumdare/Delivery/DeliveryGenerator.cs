using System.Collections.Generic;
using LudumDare.TimeControl;
using UnityEngine;

namespace LudumDare.Delivery
{
    
    public class DeliveryGenerator: MonoBehaviour
    {
        [SerializeField]
        private TimeControlManagerSocket controlManagerSocket;

        [SerializeField]
        private DeliveryResolver resolver;
        
        public void DoGenerate(int cycle)
        {
            if (cycle == 0) return;

            resolver.ExecuteDelivery(new List<DeliveryCommand> {
                new DeliveryCommand(new Vector2Int(4, 2), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(-4, 3), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(-6, 3), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(0, 5), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(4, 2), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(-4, 3), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(-6, 3), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(0, 5), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(4, 2), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(-4, 3), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(-6, 3), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(0, 5), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(4, 2), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(-4, 3), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(-6, 3), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(0, 5), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(4, 2), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(-4, 3), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(-6, 3), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(0, 5), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(4, 2), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(-4, 3), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(-6, 3), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(0, 5), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(4, 2), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(-4, 3), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(-6, 3), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(0, 5), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(4, 2), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(-4, 3), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(-6, 3), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(0, 5), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(4, 2), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(-4, 3), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(-6, 3), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(0, 5), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(4, 2), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(-4, 3), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(-6, 3), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(0, 5), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(4, 2), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(-4, 3), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(-6, 3), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(0, 5), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(4, 2), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(-4, 3), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(-6, 3), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(0, 5), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(4, 2), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(-4, 3), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(-6, 3), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(0, 5), Units.DeliveryType.Mail),
            });
        }
    }
}