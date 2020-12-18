using System.ComponentModel;

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
            OnDataChanged(sender.GetType().ToString(), ChangeInfo.ItemChanged);
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

        public void Remove(V5Data item)
        {
            item.PropertyChanged -= CollectionChangesHandler;
            listV5Data.Remove(item);
            OnDataChanged(item.GetType().ToString(), ChangeInfo.Remove);
        }
    }
}