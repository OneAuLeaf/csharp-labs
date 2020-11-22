using System;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;

namespace Lab
{
    public abstract class V5Data: IEnumerable<DataItem>
    {
        public string MetaData { get; set; }
        public DateTime DateMod { get; set; }

        public V5Data(string data, DateTime date)
        {
            MetaData = data;
            DateMod = date;
        }

        public abstract Vector2[] NearEqual(float eps);
        public abstract string ToLongString(string format);
        public abstract string ToLongString();
        protected abstract IEnumerator<DataItem> Generator();

        public virtual string ToString(string format)
        {
            return base.ToString() + ":\n\t" + 
                    $"Info: {MetaData}\tDate: {DateMod}";

        }

        public override string ToString()
        {
            return ToString(null);
        }

        IEnumerator<DataItem> IEnumerable<DataItem>.GetEnumerator()
        {
            return Generator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Generator();
        }
    }
}