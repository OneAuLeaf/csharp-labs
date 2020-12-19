using System;

namespace Lab
{
    class Program
    {
        static void Logger(object sender, DataChangedEventArgs args)
        {
            Console.WriteLine(args);
        }

        static void Main(string[] args)
        {
            V5MainCollection dataMain = new V5MainCollection();
            dataMain.DataChanged += Logger;

            // add test
            dataMain.AddDefaults();
            // item change test
            dataMain[0].MetaData = "changed";
            dataMain[2].DateMod = DateTime.Now;
            // replace test
            dataMain[2] = new V5DataCollection("replaced", new DateTime());
            // remove test
            dataMain.Remove("empty", new DateTime());
        }
    }
}