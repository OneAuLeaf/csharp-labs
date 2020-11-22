using System;
using System.Linq;

namespace Lab
{
    class Program
    {
        static void Main(string[] args)
        {
            string format = "f2";
            // test 0 (error)
            Console.WriteLine("Test exceptions here");
            string error = @"./tests./test_error.txt";
            V5DataOnGrid dataError = new V5DataOnGrid(error);
            Console.WriteLine(dataError.ToLongString(format));

            // test 1
            Console.WriteLine("Test V5DataOnGrid file constructor");
            string filename = @"./tests./test_0.txt";
            V5DataOnGrid dataGrid = new V5DataOnGrid(filename);
            Console.WriteLine(dataGrid.ToLongString(format));

            // test 2
            Console.WriteLine("Test V5MainCollection type:");
            V5MainCollection dataMain = new V5MainCollection();
            dataMain.AddDefaults();
            Console.WriteLine(dataMain.ToLongString(format));

            // test 3
            Console.WriteLine("Test V5MainCollection LINQ selects:");
            Console.WriteLine("Min lenght in V5MainCollection : " + 
                    (dataMain.MinLenght ?? 0f).ToString(format));
            Console.WriteLine("Items with min lenght in V5MainCollection :\n\t" + 
                    string.Join("\n\t", from x in dataMain.MinLenghtIterator select x.ToString(format)));
            Console.WriteLine("Points that are in V5OnDataGrid and not in V5DataCollection :\n\t" + 
                    string.Join("\n\t", from x in dataMain.ExceptIterator select x.ToString(format)));
        }
    }
}