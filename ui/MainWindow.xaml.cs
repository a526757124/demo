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

namespace ui
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

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void menu_Click(object sender, RoutedEventArgs e)
        {
            menu1.IsOpen = true;
        }

        private void x_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ___Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ksl_Click(object sender, RoutedEventArgs e)
        {
            //if (list1.Items.Count > 2)
            //{
            //    return;
            //}
            //list1.Items.Add("撸一把!");
            //list1.Items.Add("大家一起撸!");
            //list1.Items.Add("一起撸!一起撸!");
            //list1.Items.Add("撸下来!");
            //list1.Items.Add("撸下来!撸下来!");
            //list1.Items.Add("撸个痛快!");
            //list1.Items.Add("看不清了!");
            //list1.Items.Add("撸的太多了吗？");
            //list1.Items.Add("撸大师！");
            //list1.Items.Add("一起撸大师！");
        }
    }
}
