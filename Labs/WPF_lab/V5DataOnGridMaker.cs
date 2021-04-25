using System;
using System.ComponentModel;
using System.Collections.Specialized;
using Lab;

namespace WPF_lab
{
    class V5DataOnGridMaker : IDataErrorInfo, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private V5MainCollection binded;
        private string info =$"id_{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}";
        private int nx = 4, ny = 3;
        private float step = 0.5f;

        public bool HasBind => binded != null;
        public string Info
        {
            get { return info; }
            set
            {
                info = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Info"));
            }
        }
        public int Nx
        {
            get { return nx; }
            set
            {
                nx = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Nx"));
            }
        }
        public int Ny
        {
            get { return ny; }
            set
            {
                ny = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Ny"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Nx"));
            }
        }
        public float Step
        {
            get { return step; }
            set
            {
                step = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Step"));
            }
        }

        void CollectionChangedHandler(object sender, NotifyCollectionChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Info"));
        }

        public V5DataOnGridMaker() { binded = null; }
        public V5DataOnGridMaker(V5MainCollection coll)
        {
            binded = coll;
            coll.CollectionChanged += CollectionChangedHandler;
        }

        public void AddCustom()
        {
            V5DataOnGrid tmp = new V5DataOnGrid(new Grid2D(step, step, nx, ny), info, DateTime.Now);
            tmp.InitRandom(-1.0f, 1.0f);
            binded.Add(tmp);   
        }

        public string Error => this["Info"] ?? this["Nx"] ?? this["Ny"] ?? this["Step"];

        public string this[string property]
        {
            get
            {
                if (binded == null) return null;

                string msg = null;
                switch (property)
                {
                    case "Info":
                        if (Info == null || Info.Length == 0) msg = "Info should be not empty";
                        if (binded.Contains(Info)) msg = "Info should be unique";
                        break;
                    case "Nx":
                        if(Nx <= Ny) msg = "Number of nodes along Ox should be > number of nodes along Oy";
                        break;
                    case "Ny":
                        if (Ny <= 2) msg = "Number of nodes along Oy should be > 2";
                        break;
                    case "Step":
                    default:
                        break;
                }
                return msg;
            }
        }
    }
}
