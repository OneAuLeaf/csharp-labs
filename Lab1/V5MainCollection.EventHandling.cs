using System.ComponentModel;
using System.Linq;
using System;

namespace Lab
{
    public partial class V5MainCollection
    {
        public event DataChangedEventHandler DataChanged;

        protected void OnDataChanged(string type, ChangeInfo info)
        {
            if (DataChanged != null) {
                DataChanged(this, new DataChangedEventArgs(type, info));
            }
        }

        void CollectionChangesHandler(object sender, PropertyChangedEventArgs args)
        {
            OnDataChanged(sender.GetType().ToString() + " " + args.PropertyName, ChangeInfo.ItemChanged);
        }

        public V5Data this[int j]
        {
            get { return listV5Data[j]; }
            set 
            {
                listV5Data[j] = value;
                OnDataChanged(value.GetType().ToString(), ChangeInfo.Replace);
            }
        }

        public void Add(V5Data item)
        {
            listV5Data.Add(item);
            item.PropertyChanged += CollectionChangesHandler;
            OnDataChanged(item.GetType().ToString(), ChangeInfo.Add);
        }

        public bool Remove(string id, DateTime date)
        {
            var removable = from item in listV5Data where item.MetaData == id && item.DateMod == date select item;
            
            if (removable == null) {
                return false;
            }
            
            foreach (var item in removable.ToList()) {
                item.PropertyChanged -= CollectionChangesHandler;
                listV5Data.Remove(item);
                OnDataChanged(item.GetType().ToString(), ChangeInfo.Remove);
            }

            return true;   
        }
    }
}