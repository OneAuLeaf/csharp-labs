using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Runtime.Serialization.Formatters.Binary;

namespace Lab
{
    public partial class V5MainCollection: INotifyCollectionChanged, INotifyPropertyChanged
    {
        [field:NonSerialized]
        public event DataChangedEventHandler DataChanged;
        [field:NonSerialized]
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnDataChanged(string type, ChangeInfo info)
        {
            if (DataChanged != null) {
                DataChanged(this, new DataChangedEventArgs(type, info));
            }
        }

        protected void OnCollectionChanged(object sender)
        {
            if (CollectionChanged != null) {
                CollectionChanged(sender, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        protected void OnPropertyChanged(object sender, string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(sender, new PropertyChangedEventArgs(propertyName));
            }
        }

        void OnDataChangedHandler(object sender, PropertyChangedEventArgs args)
        {
            OnDataChanged(sender.GetType().ToString() + " " + args.PropertyName, ChangeInfo.ItemChanged);
            OnCollectionChanged(this);
        }

        void CollectionChangedHandler(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (sender is BinaryFormatter) {
                IsChanged = false;
            } else {
                IsChanged = true;
            }
            OnPropertyChanged(this, "IsChanged");
            OnPropertyChanged(this, "MinLenght");
        }

        public V5Data this[int j]
        {
            get { return listV5Data[j]; }
            set 
            {
                listV5Data[j] = value;
                OnDataChanged(value.GetType().ToString(), ChangeInfo.Replace);
                OnCollectionChanged(this);
            }
        }

        public void Add(V5Data item)
        {
            listV5Data.Add(item);
            item.PropertyChanged += OnDataChangedHandler;
            OnDataChanged(item.GetType().ToString(), ChangeInfo.Add);
            OnCollectionChanged(this);
        }

        public bool Remove(string id, DateTime date)
        {
            var removable = from item in listV5Data where item.MetaData == id && item.DateMod == date select item;
            
            if (removable == null) {
                return false;
            }
            
            foreach (var item in removable.ToList()) {
                item.PropertyChanged -= OnDataChangedHandler;
                listV5Data.Remove(item);
                OnDataChanged(item.GetType().ToString(), ChangeInfo.Remove);
                OnCollectionChanged(this);
            }

            return true;   
        }
    }
}