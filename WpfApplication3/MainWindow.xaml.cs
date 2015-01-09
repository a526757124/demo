using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace WpfApplication3
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            List<VModel> vs = new List<VModel>();
            VModel v1 = new VModel();
            v1.Name = "Sean";
            v1.Desciption = new List<string>();
            v1.Desciption.Add("1");
            v1.Desciption.Add("2");
            v1.Desciption.Add("3");
            vs.Add(v1);
            dataGrid1.ItemsSource = vs;

            
            
        }

        private void dataGrid1_LostFocus(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //第一种方式
            DataGridTemplateColumn tempColumn = dataGrid1.Columns[1] as DataGridTemplateColumn;
            DataTemplate dtemp = tempColumn.CellTemplate;

            ComboBox cb = (ComboBox)dtemp.LoadContent();


            //第二种方式
            DataGridTemplateColumn templeColumn = dataGrid1.Columns[1] as DataGridTemplateColumn;
            if(templeColumn == null) return;
            object item = dataGrid1.Items[0];
            FrameworkElement element = templeColumn.GetCellContent(item);
            ComboBox c = templeColumn.CellTemplate.FindName("cb", element) as ComboBox;
            
        }
       
    }

    public class VModel : INotifyPropertyChanged
    {
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                    _Name = value;
                OnPropertyChanged("Name");
            }
        }

        private List<string> _Desciption;

        public List<string> Desciption
        {
            get { return _Desciption; }
            set {
                if (_Desciption != value)
                    _Desciption = value;
                OnPropertyChanged("Desciption");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName)) throw new ArgumentNullException("propertyName");
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}
