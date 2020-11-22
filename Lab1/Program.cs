using System;

namespace Lab
{
    class Program
    {
        static void Main(string[] args)
        {
            // test 1
            Console.WriteLine("Test V5DataOnGrid file constructor");
            string format = "f4";
            string filename = null;
            V5DataOnGrid dataGrid = new V5DataOnGrid(filename);
            Console.WriteLine(dataGrid.ToLongString(format));

            // test 2
            Console.WriteLine("Test V5MainCollection type:");
            V5MainCollection dataMain = new V5MainCollection();
            dataMain.AddDefaults();
            Console.WriteLine(dataMain.ToLongString(format));
            Console.WriteLine();

            // test 3
            Console.WriteLine("Test V5MainCollection LINQ selects:");
            Console.WriteLine($"Min lenght in V5DataCollection : {dataMain.MinLenght.ToString(format)}");
            Console.WriteLine($"Items with min lenght in V5MainCollection : {string.Join(", ", dataMain.MinLenghtDataGenerator)}");
            Console.WriteLine($"Points that are in V5OnDataGrid and not in V5DataCollection : {string.Join(", ", dataMain.MinLenghtDataGenerator)}");
        }
    }
}