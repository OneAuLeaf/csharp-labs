using System;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lab
{
    public class V5MainCollection: IEnumerable<V5Data>
    {
        private List<V5Data> listV5Data = new List<V5Data>();
        public int Count => listV5Data.Count;
        public float? MinLenght => 
            (from data in listV5Data
            where data.Count() > 0
            select data.Min(z => z.EMValue.Length())).Min(z => (float?) z);
        public IEnumerable<DataItem> MinLenghtIterator => 
            from data in listV5Data
            from item in data
            where item.EMValue.Length() == MinLenght
            select item;
        public IEnumerable<Vector2> ExceptIterator =>
            (from data in listV5Data
            where data is V5DataOnGrid
            from item in data
            select item.Point)
            .Except
            (from data in listV5Data
            where data is V5DataCollection
            from item in data
            select item.Point);

        public void Add(V5Data item)
        {
            listV5Data.Add(item);
        }

        public bool Remove(string id, DateTime date)
        {
            if (listV5Data.RemoveAll(item => item.MetaData == id && item.DateMod == date) > 0) {
                return true;
            }
            return false;
        }

        public void AddDefaults()
        {
            listV5Data.Add(new V5DataOnGrid(@"./tests/test_0.txt"));
            listV5Data.Add(new V5DataOnGrid(@"./tests/test_1.txt"));
            listV5Data.Add(new V5DataOnGrid(@"./tests/test_empty.txt"));
            listV5Data.Add((V5DataCollection) new V5DataOnGrid(@"./tests/test_1.txt"));
            listV5Data.Add(new V5DataCollection($"empty", new DateTime()));
        }

        public string ToLongString(string format)
        {
            return string.Join("\n", from data in listV5Data select data.ToLongString(format));
        }

        public override string ToString()
        {
            return string.Join("\n", listV5Data);
        }

        IEnumerator<V5Data> IEnumerable<V5Data>.GetEnumerator()
        {
            return listV5Data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return listV5Data.GetEnumerator();
        }
        
    }
}