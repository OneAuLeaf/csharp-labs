using System;
using System.Linq;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;

namespace Lab
{
    [Serializable]
    public partial class V5MainCollection: IEnumerable<V5Data>
    {
        private List<V5Data> listV5Data;

        public bool IsChanged { get; private set; }
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

        public V5MainCollection()
        {
            listV5Data = new List<V5Data>();
            IsChanged = false;
            CollectionChanged += CollectionChangedHandler;
        }

        public void AddDefaults()
        {
            V5DataOnGrid test0 = new V5DataOnGrid(new Grid2D(1.0f, 1.0f, 3, 3), "default", DateTime.Now);
            test0.InitRandom(-1.0f, 1.0f);
            Add(test0);
            Add((V5DataCollection)test0);
            Add(new V5DataOnGrid(new Grid2D(0.5f, 0.5f, 2, 2), "empty", DateTime.Now));
            Add(new V5DataCollection("empty", new DateTime()));
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