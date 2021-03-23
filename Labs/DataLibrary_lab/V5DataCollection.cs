using System;
using System.Numerics;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Lab
{
    [Serializable]
    public class V5DataCollection: V5Data, IEnumerable<DataItem>, ISerializable
    {
        public Dictionary<Vector2, Vector2> GridValues { get; set; }

        public V5DataCollection(string data, DateTime date): base(data, date)
        {
            GridValues = new Dictionary<Vector2, Vector2>();
        }

        public void InitRandom(int nItems, float xmax, float ymax, float minValue, float maxValue)
        {
            for (int i = 0; i < nItems; ++i) {
                Vector2 key = Randomizer.GenerateValue(0, xmax, 0, ymax);
                Vector2 value = Randomizer.GenerateValue(maxValue, minValue);
                GridValues.Add(key, value);
            }
        }

        public override Vector2[] NearEqual(float eps)
        {
           var res = new List<Vector2>();
            foreach (var item in GridValues) {
                if (Math.Abs(item.Value.X - item.Value.Y) < eps) {
                    res.Add(item.Value);
                }
            }
            return res.ToArray();
        }

        public override string ToString(string format)
        {
            return base.ToString(format) + "\n\t" + 
                $"Items count:\t{GridValues.Count.ToString()}";
        }

        public override string ToString()
        {
            return ToString(null);
        }

        public override string ToLongString(string format)
        {
            string str = ToString(format) + "\nItems:\n\t";
            foreach (var item in this) {
                str += item.ToString(format) + "\n\t";
            }
            return str;
        }

        public override string ToLongString()
        {
            return ToLongString(null);
        }

        protected override IEnumerator<DataItem> Generator()
        {
            foreach (var item in GridValues) {
                yield return new DataItem(item.Key, item.Value);
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("V5Data_MetaData", MetaData);
            info.AddValue("V5Data_DateMod", DateMod);
            info.AddValue("GridValues_Count", GridValues.Count); // for deserialization
            int i = 0;
            foreach (var item in this) {
                info.AddValue($"item_{i}", item, typeof(DataItem));
                ++i;
            }

        }

        public V5DataCollection(SerializationInfo info, StreamingContext context)
                : base(info.GetString("V5Data_MetaData"), info.GetDateTime("V5Data_DateMod"))
        {
            GridValues = new Dictionary<Vector2, Vector2>();
            int count = info.GetInt32("GridValues_Count");
            for (int i = 0; i < count; ++i) {
                var item = (DataItem) info.GetValue($"item_{i}", typeof(DataItem));
                GridValues.Add(item.Point, item.EMValue);
            }
        }
    }
}