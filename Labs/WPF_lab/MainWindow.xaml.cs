using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;
using Microsoft.Win32;
using Lab;

namespace WPF_lab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private V5MainCollection mainCollection;

        public MainWindow()
        {
            InitializeComponent();
            CultureInfo.CurrentCulture = new CultureInfo("ru-RU");
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            mainCollection = new V5MainCollection();
            this.DataContext = mainCollection;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (SaveBeforeChange() == null) {
                e.Cancel = true;
            }
        }

        private bool? SaveBeforeChange()
        {
            bool? answer = AskSave();
            if (answer == true)
            {
                Save_Click(this, null);
            }
            return answer;
        }

        private bool? AskSave()
        {
            if (mainCollection.IsChanged)
            {
                MessageBoxResult res = MessageBox.Show("Есть несохраненные изменения. Желаете сохранить их?", 
                        "Несохраненные изменения",  MessageBoxButton.YesNoCancel);
                if (res == MessageBoxResult.Cancel) {
                    return null;
                } else if (res == MessageBoxResult.Yes) {
                    return true;
                }
            }
            return false;
        }

        private void FilterDataOnGrid(object sender, FilterEventArgs args) => args.Accepted = args.Item is V5DataOnGrid;
        private void FilterDataCollection(object sender, FilterEventArgs args) => args.Accepted = args.Item is V5DataCollection;


        private void New_Click(object sender, RoutedEventArgs e)
        {
            if (SaveBeforeChange() == null) {
                return;
            }
            mainCollection = new V5MainCollection();
            this.DataContext = mainCollection;
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            if (SaveBeforeChange() == null) {
                return;
            }

            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                if (dlg.ShowDialog() == true) {
                    mainCollection.Load(dlg.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
                if (dlg.ShowDialog() == true) {
                    mainCollection.Save(dlg.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }

        private void AddDefaults_Click(object sender, RoutedEventArgs e)
        {
            mainCollection.AddDefaults();
        }

        private void AddDefaultDataCollection_Click(object sender, RoutedEventArgs e)
        {
            V5DataCollection data = new V5DataCollection("default V5DataCollection", DateTime.Now);
            data.InitRandom(4, 2.0f, 2.0f, -1.0f, 1.0f);
            mainCollection.Add(data);
        }

        private void AddDefaultDataOnGrid_Click(object sender, RoutedEventArgs e)
        {
            V5DataOnGrid data = new V5DataOnGrid(new Grid2D(0.5f, 0.5f, 2, 2), "default V5DataOnGrid", DateTime.Now);
            data.InitRandom(-1.0f, 1.0f);
            mainCollection.Add(data);
        }

        private void AddElementFromFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == true) {
                try
                {
                    V5DataOnGrid data = new V5DataOnGrid(dlg.FileName);
                    mainCollection.Add(data);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message);
                }
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (listBox_Main.SelectedItem != null) {
                V5Data data = listBox_Main.SelectedItem as V5Data;
                mainCollection.Remove(data.MetaData, data.DateMod);
            }
        }
    }
}
