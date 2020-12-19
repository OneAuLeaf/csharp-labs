using System;
using System.Numerics;
using System.Collections.Generic;
using System.IO;
using System.Globalization;

namespace Lab
{
    public class V5DataOnGrid: V5Data, IEnumerable<DataItem>
    {   
        public Grid2D Grid { get; set; }
        public Vector2[,] GridValues { get; set; }

        public V5DataOnGrid(Grid2D grid, string data, DateTime date): base(data, date)
        {
            Grid = grid;
            GridValues = new Vector2[Grid.Nx, Grid.Ny];
        }


        // template in ./tests/template.pdf
        public V5DataOnGrid(string filename) : base("", new DateTime())
        {
            var culture = CultureInfo.CurrentCulture;

            CultureInfo.CurrentCulture = new CultureInfo("ru-RU");
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.Open);
                StreamReader reader = new StreamReader(fs);
                var base_line = reader.ReadLine();
                if (base_line == null) {
                    throw new Exception("V5Data arguments not found");
                }
                var grid_line = reader.ReadLine();
                if (grid_line == null) {
                    throw new Exception("Grid2D arguments not found");
                }
                var base_arg = base_line.Split(";");
                if (base_arg.Length - 1 < 2) {
                        throw new Exception($"Not enough V5Data arguments, {base_arg.Length - 1} found, while 2 expected");
                }
                var grid_arg = grid_line.Split(";");
                if (grid_arg.Length - 1 < 4) {
                        throw new Exception($"Not enough Grid2D arguments, {grid_arg.Length - 1} found, while 4 expected");
                }

                MetaData = base_arg[0];
                DateMod = DateTime.Parse(base_arg[1]);
                Grid = new Grid2D(float.Parse(grid_arg[0]), float.Parse(grid_arg[1]), int.Parse(grid_arg[2]), int.Parse(grid_arg[3]));

                GridValues = new Vector2[Grid.Nx, Grid.Ny];
                for (int i = 0; i < Grid.Nx; ++i) {
                    var line = reader.ReadLine();
                    if (line == null) {
                        throw new Exception($"Not enough rows, {i} found, while {Grid.Nx} expected");
                    }
                    var args = line.Split(";");
                    if (args.Length - 1 < Grid.Ny) {
                        throw new Exception($"Not enough columns, {args.Length - 1} found, while {Grid.Ny} expected");
                    }
                    for (int j = 0; j < Grid.Ny; ++j) {
                        var vector  = args[j].Split(" ");
                        if (vector.Length < 2) {
                            throw new Exception($"Not enough coords, {vector.Length} found, while 2 expected");
                        }
                        GridValues[i, j] = new Vector2(float.Parse(vector[0]), float.Parse(vector[1]));
                    }
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                MetaData = "error occured";
                DateMod = new DateTime();
                Grid = new Grid2D(0, 0, 0, 0);
                GridValues = new Vector2[0, 0];
            }
            finally
            {
                if (fs != null) {
                    fs.Close();
                }
                CultureInfo.CurrentCulture = culture;
            }
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

        public override string ToString(string format)
        {
            return base.ToString(format) + "\n\t" + 
                    Grid.ToString(format);
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
            for (int x = 0; x < GridValues.GetLength(0); ++x) {
                for (int y = 0; y < GridValues.GetLength(1); ++y) {
                    yield return new DataItem(new Vector2(Grid.Ox * x, Grid.Oy * y), GridValues[x, y]);
                }
            }
        }
    }
}