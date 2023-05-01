using System;
using UnityEngine;

namespace LudumDare.Delivery
{
    public class RandomMathFunction
    {


        private static readonly float BASE_SCALING = 0.003f;
        private static readonly float NOISE_FREQUENCY = 0.04f;
        private static readonly float NOISE_AMPLITUDE = 5;

        public RandomMathFunction()
        {

        }

        public float GetRandomValue(float x)
        {
            return BASE_SCALING * x * x + NOISE_AMPLITUDE * Mathf.PerlinNoise1D(x * NOISE_FREQUENCY);
        }

        public float GetAvgForDay(float x)
        {
            return GetRandomValue(x);
        }

    }
}