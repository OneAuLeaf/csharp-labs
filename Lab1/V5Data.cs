using System;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Lab
{
    public abstract class V5Data: INotifyPropertyChanged, IEnumerable<DataItem>
    {
        private string metaData;
        private DateTime dateMod;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string MetaData
        { 
            get { return metaData; } 
            set 
            {
                metaData = value;
                OnPropertyChanged("MetaData");
            } 
        }
        public DateTime DateMod
        { 
            get { return dateMod; } 
            set 
            {
                dateMod = value;
                OnPropertyChanged("DateMod");
            } 
        }

        public V5Data(string data, DateTime date)
        {
            MetaData = data;
            DateMod = date;
        }

        public abstract Vector2[] NearEqual(float eps);
        public abstract string ToLongString(string format);
        public abstract string ToLongString();
        protected abstract IEnumerator<DataItem> Generator();

        public virtual string ToString(string format)
        {
            return base.ToString() + ":\n\t" + 
                    $"Info: {MetaData}\tDate: {DateMod}";

        }

        public override string ToString()
        {
            return ToString(null);
        }

        IEnumerator<DataItem> IEnumerable<DataItem>.GetEnumerator()
        {
            return Generator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Generator();
        }
    }
}