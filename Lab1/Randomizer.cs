using System;
using System.Numerics;

namespace Lab
{
    static class Randomizer
    {
        private static Random random;

        static Randomizer()
        {
            random = new Random();
        }

        private static float GenerateInRange(float min, float max)
        {
            float r = (float) random.NextDouble();
            return r * (max - min) + min;
        }

        public static Vector2 GenerateValue(float xmin, float xmax, float ymin, float ymax)
        {
            return new Vector2(GenerateInRange(xmin, xmax), GenerateInRange(ymin, ymax));
        }

        public static Vector2 GenerateValue(float min, float max)
        {
            return new Vector2(GenerateInRange(min, max), GenerateInRange(min, max));      
        }

    }
}