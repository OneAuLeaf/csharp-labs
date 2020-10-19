using System;

namespace Lab
{
    class Program
    {
        static void Main(string[] args)
        {
            // test 1
            Console.WriteLine("Test V5DataOnGrid and V5DataCollection types:");
            Grid2D grid = new Grid2D(1, 1, 3, 3);
            DateTime date = DateTime.Now;
            string info = "test";
            V5DataOnGrid dataGrid = new V5DataOnGrid(grid, info, date);
            dataGrid.InitRandom(-1, 1);
            Console.WriteLine(dataGrid.ToLongString());
            V5DataCollection dataCollection = (V5DataCollection) dataGrid;
            Console.WriteLine(dataCollection.ToLongString());

            // test 2
            Console.WriteLine("Test V5MainCollection type:");
            V5MainCollection dataMain = new V5MainCollection();
            dataMain.AddDefaults();
            Console.WriteLine(dataMain);
            Console.WriteLine();

            // test 3
            float eps = 0.2f;
            Console.WriteLine("Test NearEqual(eps) method with eps = {0}:", eps);
            foreach (var item in dataMain) {
                Console.WriteLine("[{0}]", string.Join(", ", item.NearEqual(eps)));
            }
        }
    }
}