using System;
using System.Numerics;

namespace Lab
{
    public abstract class V5Data
    {
        public string MetaData { get; set; }
        public DateTime DateMod { get; set; }

        public V5Data(string data, DateTime date)
        {
            MetaData = data;
            DateMod = date;
        }

        public abstract Vector2[] NearEqual(float eps);
        public abstract string ToLongString();
        public abstract string ToLongString(string format);

        public override string ToString()
        {
            return base.ToString() + ":\n\t" + 
                    $"Info: {MetaData}\tDate: {DateMod}";
        }
    }
}