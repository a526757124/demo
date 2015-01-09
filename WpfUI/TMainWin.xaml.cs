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
using System.Windows.Shapes;

using Telerik.Windows.Controls;

namespace WpfUI
{
    /// <summary>
    /// Interaction logic for TMainWin.xaml
    /// </summary>
    public partial class TMainWin
    {
        public TMainWin()
        {
            InitializeComponent();
        }
        private void closeTabButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            for (int i = 0; i < contextTab.Items.Count; i++)
            {
                TabItem tabItem = contextTab.Items[i] as TabItem;
                if (tabItem.Header.Equals(b.Tag))
                {
                    tabItem.Content = null;
                    this.contextTab.Items.Remove(tabItem);
                }
            }
            foreach (TabItem item in contextTab.Items)
            {

            }

        }
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabItem tabItem = e.AddedItems[0] as TabItem;
            if (tabItem != null)
            {

                var tempItem = new TabItem();
                tempItem.Header = tabItem.Header;
                tempItem.Background = (Brush)System.ComponentModel.TypeDescriptor.GetConverter(
    typeof(Brush)).ConvertFromInvariantString("Azure");
                //设置按钮样式使用此种方法必须将资源文件引用到当前页面
                //btn.Style = Resources["NoticeButton"] as Style;
                //此种方法也可以设置样式
                //获取App.xaml中的样式个人更喜欢这种
                //Style style = (Style)this.FindResource("NoticeButton");
                //为按钮设置样式
                // btn.Style = style
                tempItem.Style = (Style)this.FindResource("tabCloseBtn");
                tempItem.MouseEnter += tempItem_MouseEnter;
                foreach (TabItem item in contextTab.Items)
                {
                    if (item.Header.Equals(tempItem.Header))
                    {
                        item.IsSelected = true;
                        return;
                    }
                }
                tempItem.Content = GetPage(tabItem.Tag);

                contextTab.Items.Add(tempItem);


            }
        }
        private UserControl GetPage(object type)
        {
            if (type == null) return null;
            switch (type.ToString())
            {
                case "JournalVoucher":
                    return new UCJournalVoucher();
                default:
                    return null;
            }
        }
        void tempItem_MouseEnter(object sender, MouseEventArgs e)
        {
            foreach (TabItem item in contextTab.Items)
            {
                var v = GetChildObjects(item);
                foreach (Button btn in v)
                {
                    if (btn == null) continue;
                    btn.Click -= closeTabButton_Click;
                    btn.Click += closeTabButton_Click;
                }
            }
        }
        public List<Button> GetChildObjects(DependencyObject obj)
        {
            DependencyObject child = null;
            List<Button> childList = new List<Button>();

            for (int i = 0; i <= VisualTreeHelper.GetChildrenCount(obj) - 1; i++)
            {
                child = VisualTreeHelper.GetChild(obj, i);

                if (child is Button && (((Button)child).GetType() == typeof(Button)))
                {
                    childList.Add((Button)child);
                }
                childList.AddRange(GetChildObjects(child));
            }
            return childList;
        }
    }
}
