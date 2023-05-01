using System;
using System.Collections.Generic;
using LudumDare.Districts;
using LudumDare.TimeControl;
using UnityEngine;
using Random = System.Random;

namespace LudumDare.Delivery
{
    
    public class DeliveryGenerator: MonoBehaviour
    {
        [SerializeField]
        private TimeControlManagerSocket controlManagerSocket;
        [SerializeField]
        private DistrictManagerSocket districtManagerSocket;

        [SerializeField]
        private DeliveryResolver resolver;
        
        public void DoGenerate(int cycle)
        {
            if (cycle == 0) return;

            resolver.ExecuteDelivery(new List<DeliveryCommand> {
                new DeliveryCommand(new Vector2Int(4, 2), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(-4, 3), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(-6, 3), Units.DeliveryType.Mail),
                new DeliveryCommand(new Vector2Int(-6, 3), Units.DeliveryType.Mail),
            });
        }
    }
    
    public class RandomMathFunction
    {
        private readonly Function f1;
        private readonly Function f2;

        public RandomMathFunction()
        {
            f1 = new Function(0.5f, 0.2f, 1f, 5f);
            f2 = new Function(0.5f, 0.25f, 1f, 4.8f);
        }

        public float GetRandomValue(float x)
        {
            var days = (int) Math.Round(x / 60 / 24);
            var random = new Random(days);
            if (random.Next(2) == 0)
            {
                return f1.GetValue(days);
            }
            else
            {
                return f2.GetValue(days);
            }
        }

        public float GetAvgForDay(float x)
        {
            var days = (int) Math.Round(x / 60 / 24);
            return (f1.GetValue(days) + f2.GetValue(days)) / 2;
        }

        private class Function
        {
            private readonly float _a;
            private readonly float _b;
            private readonly float _c;
            private readonly float _d;

            public Function(float a, float b, float c, float d)
            {
                _a = a;
                _b = b;
                _c = c;
                _d = d;
            }

            public float GetValue(float x)
            {
                return _a * x + (float) Math.Sin(_b * x) + (float) Math.Sin(_c * x) + (float) Math.Sin(_d * x);
            }
        }
    }
}