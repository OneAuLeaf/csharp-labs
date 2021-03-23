using System;

namespace Lab
{
    public enum ChangeInfo { ItemChanged, Add, Remove, Replace };

    public delegate void DataChangedEventHandler(object source, DataChangedEventArgs args);

    public class DataChangedEventArgs: EventArgs
    {
        public ChangeInfo ChangeInfo { get; set; }
        public string ElementType { get; set; }

        public DataChangedEventArgs(string type, ChangeInfo info)
        {
            ChangeInfo = info;
            ElementType = type;
        }

        public override string ToString()
        {
            return ChangeInfo + " " + ElementType;
        }
    }
}