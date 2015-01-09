using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace wpfDemo
{
    /// <summary>
    /// grid.xaml 的交互逻辑
    /// </summary>
    public partial class grid : Window
    {
        public grid()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TabItem item = new TabItem();
            item.Content="name";
            
            this.tab.Items.Add(item);
        }
        //关闭选项卡
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string header = btn.Tag.ToString();
            foreach (TabItem item in this.tab.Items)
            {
                if (item.Header.ToString() == header)
                {
                    this.tab.Items.Remove(item);
                    break;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (TabItem item in this.tab.Items)
            {
                if (item.Header.ToString() == "主页")
                {
                    var v= item.Content;
                }
            }
        }
    }
}
