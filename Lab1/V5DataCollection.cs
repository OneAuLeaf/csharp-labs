using System;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;

namespace Lab
{
    public class V5DataCollection: V5Data, IEnumerable<DataItem>
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

        public override string ToString()
        {
            return base.ToString() + "\n\t" + 
                $"Items count:\t{GridValues.Count.ToString()}";
        }

        public override string ToLongString(string format)
        {
            string str = ToString() + "\nItems:\n";
            foreach (var item in GridValues) {
                str += $"\t({item.Key.X.ToString(format)}, {item.Key.Y.ToString(format)}) = <{item.Value.X.ToString(format)}, {item.Value.Y.ToString(format)}>\n";
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
    }
}