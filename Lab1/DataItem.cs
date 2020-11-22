using System.Numerics;

namespace Lab
{
    public struct DataItem
    {
        public Vector2 Point { get; set; }
        public Vector2 EMValue { get; set; }

        public DataItem(Vector2 point, Vector2 emvalue)
        {
            Point = point;
            EMValue = emvalue;
        }

        public string ToString(string format)
        {
            return $"Point: {Point.ToString(format)} EMValue: {EMValue.ToString(format)} Length: {EMValue.Length().ToString(format)}";
        }

        public override string ToString()
        {
            return ToString(null);
        }
    }
}