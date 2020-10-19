using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab
{
    public class V5MainCollection: IEnumerable<V5Data>
    {
        private List<V5Data> listV5Data = new List<V5Data>();
        public int Count => listV5Data.Count;

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
            for (int i = 0; i < 2; ++i) {
                var grid = new Grid2D(1, 1, 5, 5);
                var temp = new V5DataOnGrid(grid, $"test1{i}", new DateTime());
                temp.InitRandom(-1, 1);
                listV5Data.Add(temp);
            }
            for (int i = 0; i < 2; ++i) {
                var temp = new V5DataCollection($"test2{i}", new DateTime());
                temp.InitRandom(10, 5, 5, -1, 1);
                listV5Data.Add(temp);
            }
        }

        IEnumerator<V5Data> IEnumerable<V5Data>.GetEnumerator()
        {
            return listV5Data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return listV5Data.GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join("\n", listV5Data);
        }
    }
}