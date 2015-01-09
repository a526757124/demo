using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{


    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //IList li = new TestModel[] { };
            dg.CellEditEnding += dg_CellEditEnding;
            dg.RowEditEnding += dg_RowEditEnding;
            this.DataContext =viewModel= new MainWindowViewModel();
           
        }

        private MainWindowViewModel viewModel;
        void dg_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {

            }
        }


        void dg_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var fff= e.Column.GetCellContent(e.Row);
            if (fff != null && fff.BindingGroup != null && fff.BindingGroup.Items.Count > 0)
            {
                bool flag= fff.BindingGroup.CommitEdit();
                Console.WriteLine(flag);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
             　
        }


        // private IEnumerable<MM> Source;
    }
    public class MainWindowViewModel :System.ComponentModel.INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            Items = new ActionList<TestModel>(new[]
                                            {
                                                new TestModel() {A1 = "1"},
                                                 new TestModel() {A1 = "22"} ,
                                                 new TestModel() {A1 = "33"}
                                            });
            for (int i = 0; i < 999; i++)
            {
                  Items.Add(new TestModel(){A1 = i.ToString()});
            }
        }
        public ActionList<TestModel> Items { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
    public class TestModel : System.ComponentModel.INotifyPropertyChanged
    {
        private string _a1;
        public string A1
        {
            get { return _a1; }
            set
            {
                _a1 = value;
                OnPropertyChanged("A1"); 
                OnPropertyChanged("A3");
            }
        }

        private string _a2;
        public string A2
        {
            get { return _a2; }
            set
            {
                _a2 = value;  
                OnPropertyChanged("A2");
                OnPropertyChanged("A3");
            }
        }

        public string A3 { get { return A1 + A2; } }
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyname)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
