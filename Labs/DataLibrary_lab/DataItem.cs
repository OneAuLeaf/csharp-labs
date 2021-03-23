using System;
using System.Numerics;
using System.Runtime.Serialization;

namespace Lab
{
    [Serializable]
    public struct DataItem: ISerializable
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
            return $"Point: {Point.ToString(format)}\tEMValue: {EMValue.ToString(format)}\tLength: {EMValue.Length().ToString(format)}";
        }

        public override string ToString()
        {
            return ToString(null);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Point_X", Point.X);
            info.AddValue("Point_Y", Point.Y);
            info.AddValue("EMValue_X", EMValue.X);
            info.AddValue("EMValue_Y", EMValue.Y);
        }

        public DataItem(SerializationInfo info, StreamingContext context)
        {
            float px = info.GetSingle("Point_X");
            float py = info.GetSingle("Point_Y");
            float vx = info.GetSingle("EMValue_X");
            float vy = info.GetSingle("EMValue_Y");
            Point = new Vector2(px, py);
            EMValue = new Vector2(vx, vy);
        }
    }
}