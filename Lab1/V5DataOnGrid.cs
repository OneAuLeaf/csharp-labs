using System;
using System.Numerics;
using System.Collections.Generic;

namespace Lab
{
    public class V5DataOnGrid: V5Data
    {
        public Grid2D Grid { get; set; }
        public Vector2[,] GridValues { get; set; }

        public V5DataOnGrid(Grid2D grid, string data, DateTime date): base(data, date)
        {
            Grid = grid;
            GridValues = new Vector2[Grid.Nx, Grid.Ny];
        }

        public static explicit operator V5DataCollection(V5DataOnGrid instance)
        {
            var res = new V5DataCollection(instance.MetaData, instance.DateMod);
            for (int x = 0; x < instance.GridValues.GetLength(0); ++x) {
                for (int y = 0; y < instance.GridValues.GetLength(1); ++y) {
                    var vec = new Vector2(x * instance.Grid.Ox, y * instance.Grid.Oy);
                    res.GridValues.Add(vec, instance.GridValues[x, y]);
                }
            }
            return res;
        }
        
        public void InitRandom(float minValue, float maxValue)
        {
            for (int i = 0; i < GridValues.GetLength(0); ++i) {
                for (int j = 0; j < GridValues.GetLength(1); ++j) {
                    GridValues[i,j] = Randomizer.GenerateValue(minValue, maxValue);
                }
            }
        }

        public override Vector2[] NearEqual(float eps)
        {
            var res = new List<Vector2>();
            foreach (var item in GridValues) {
                if (Math.Abs(item.X - item.Y) < eps) {
                    res.Add(item);
                }
            }
            return res.ToArray();
        }

        public override string ToString()
        {
            return base.ToString() + "\n\t" + 
                    Grid.ToString();
        }

        public override string ToLongString()
        {
            string str = ToString() + "\nItems:\n";
            for (int x = 0; x < GridValues.GetLength(0); ++x) {
                for (int y = 0; y < GridValues.GetLength(1); ++y) {
                    str += $"\t({x * Grid.Ox}, {y * Grid.Oy}) = <{GridValues[x, y].X}, {GridValues[x, y].Y}>\n";
                }
            }
            return str;
        }
    }
}