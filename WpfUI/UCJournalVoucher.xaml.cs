using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using WpfUI.Model;
using WpfUI.ViewModel;

namespace WpfUI
{
    /// <summary>
    /// UCJournalVoucher.xaml 的交互逻辑
    /// </summary>
    public partial class UCJournalVoucher : UserControl
    {
        private JournalVoucherViewModel viewModel;
        public UCJournalVoucher()
        {
            InitializeComponent();
            DataContext = viewModel = new JournalVoucherViewModel();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void DataGrid_RowEditEnding(object sender, Microsoft.Windows.Controls.DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == Microsoft.Windows.Controls.DataGridEditAction.Commit)
            {

            }
        }

        private void DataGrid_CellEditEnding(object sender, Microsoft.Windows.Controls.DataGridCellEditEndingEventArgs e)
        {
            var fff = e.Column.GetCellContent(e.Row);
            var auto = e.EditingElement as TextBox;

            if (fff != null)
            {
                //bool flag = fff.BindingGroup.CommitEdit();
                //Console.WriteLine(flag);
            }
        }
        private void digestAutoCompleteBox_Populating(object sender, PopulatingEventArgs e)
        {
            var auto = sender as AutoCompleteBox;
            if (auto != null)
            {
                e.Cancel = true;
                viewModel.SubjectItems.Clear();
                for (int i = 0; i < 100; i++)
                {
                    viewModel.SubjectItems.Add(new AutoCompleteModel()
                    {
                        Name = e.Parameter + i + i,
                        SerchString = e.Parameter
                    });
                }
                auto.ItemsSource = viewModel.SubjectItems;
                auto.FilterMode = AutoCompleteFilterMode.Contains;
                auto.PopulateComplete();
            }
        }
        private bool ItemFilter(string search, object item)
        {
            var subjectItem = item as AutoCompleteModel;
            //搜索匹配关键字
            if (subjectItem.Name.ToLower().Contains(search.ToLower()) || subjectItem.Code.ToLower().Contains(search.ToLower()))
            {
                return true;//返回True表示此项匹配，会出现在下拉选框中
            }
            return false;
        }
        /// <summary>
        /// 获取一个类指定的属性值
        /// </summary>
        /// <param name="info">object对象</param>
        /// <param name="field">属性名称</param>
        /// <returns></returns>
        public static object GetPropertyValue(object info, string field)
        {
            if (info == null) return null;
            Type t = info.GetType();
            IEnumerable<System.Reflection.PropertyInfo> property = from pi in t.GetProperties() where pi.Name.ToLower() == field.ToLower() select pi;
            return property.First().GetValue(info, null);
        }

        private void digestAutoCompleteBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var auto = e.Source as AutoCompleteBox;
            if (auto != null)
            {
                viewModel.Items.FirstOrDefault(p => p.Id == (int)auto.Tag).Digest = (auto.SelectedItem as AutoCompleteModel).Name;
            }
        }

        private void digestAutoCompleteBox_TextChanged(object sender, RoutedEventArgs e)
        {
            var auto = e.Source as AutoCompleteBox;
            if (auto != null)
            {
                viewModel.Items.FirstOrDefault(p => p.Id == (int)auto.Tag).Digest = auto.Text;
            }
        }

        private void subjectAutoCompleteBox_Populating(object sender, PopulatingEventArgs e)
        {
            var auto = sender as AutoCompleteBox;
            if (auto != null)
            {
                e.Cancel = true;
                viewModel.SubjectItems.Clear();
                for (int i = 0; i < 100; i++)
                {
                    viewModel.SubjectItems.Add(new AutoCompleteModel()
                    {
                        Id = i,
                        Code = "010" + i,
                        Name = "abc" + i,
                        SerchString = e.Parameter
                    });
                }
                auto.ItemsSource = viewModel.SubjectItems;
                auto.FilterMode = AutoCompleteFilterMode.Custom;
                auto.PopulateComplete();
            }
        }

        private void subjectAutoCompleteBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var auto = e.Source as AutoCompleteBox;
            if (auto != null)
            {
                viewModel.Items.FirstOrDefault(p => p.Id == (int)auto.Tag).Subject = new Subject()
                {
                    Code = (auto.SelectedItem as AutoCompleteModel).Code,
                    Name = (auto.SelectedItem as AutoCompleteModel).Name,
                    Id = 1
                };
            }
        }

        private void subjectAutoCompleteBox_TextChanged(object sender, RoutedEventArgs e)
        {
            var auto = e.Source as AutoCompleteBox;
            if (auto != null && string.IsNullOrEmpty(auto.Text))
            {
                viewModel.Items.FirstOrDefault(p => p.Id == (int)auto.Tag).Subject = new Subject()
                {
                    Code = "",
                    Name = "",
                    Id = 0
                };
            }
        }
        /// <summary>
        /// 获取DataGrid的第一个被发现的验证错误结果。
        /// </summary>
        /// <param name="dg">被检查的DataGrid实例。</param>
        /// <returns>错误结果。</returns>
        public static ValidationError GetDataGridRowsFirstError(DataGrid dg)
        {
            ValidationError err = null;
            for (int i = 0; i < dg.Items.Count; i++)
            {
                DependencyObject o = dg.ItemContainerGenerator.ContainerFromIndex(i);
                bool hasError = Validation.GetHasError(o);
                if (hasError)
                {
                    err = Validation.GetErrors(o)[0];
                    break;
                }
            }
            return err;
        }
        /// <summary>
        /// 执行检查DataGrid，并提示第一个错误。重新定位到错误单元格。
        /// </summary>
        /// <param name="dg">被检查的DataGrid实例。</param>
        /// <returns>true 有错并定位，false 无错、返回</returns>
        public static bool ExcutedCheckedDataGridValidation(DataGrid dg)
        {
            ValidationError err = GetDataGridRowsFirstError(dg);
            if (err != null)
            {
                string errColName = ((System.Windows.Data.BindingExpression)err.BindingInError).ParentBinding.Path.Path;
                DataGridColumn errCol = dg.Columns.Single(p =>
                {
                    if (((Binding)((DataGridTextColumn)p).Binding).Path.Path == errColName)
                        return true;
                    else return false;
                });
                //string errRow = ((DataRowView)((System.Windows.Data.BindingExpression)err.BindingInError).DataItem)["SWH"].ToString();
                //dg.Items.IndexOf(((System.Windows.Data.BindingExpression)err.BindingInError).DataItem);
                dg.SelectedItem = ((System.Windows.Data.BindingExpression)err.BindingInError).DataItem;
                int errRowIndex = dg.SelectedIndex;
                MessageBox.Show(string.Format("第\"{0}\"行 的\"{1}\"列的单元格数据不合法(以红色标出)，请填写正确后再执行其他操作。", errRowIndex + 1, errCol.Header), "系统消息", MessageBoxButton.OK, MessageBoxImage.Warning);

                dg.CurrentCell = new DataGridCellInfo(dg.SelectedItem, errCol);
                if (!((DataRowView)dg.CurrentItem).IsEdit)
                {
                    ((DataRowView)dg.CurrentItem).BeginEdit();

                }
                TextBox txt = dg.CurrentColumn.GetCellContent(dg.CurrentItem) as TextBox;
                txt.Focus();
                return true;
            }
            else
            {
                return false;
            }
        }


    }
    public class JournalVoucherViewModel
    {
        public JournalVoucherViewModel()
        {
            Items = new List<Voucher>();
            for (int i = 0; i < 5; i++)
            {
                Items.Add(new Voucher()
                {
                    Id = i,
                    Subject = new Subject()
                    {
                        Id = i,
                        Name = "name" + i,
                        Code = "code" + i
                    }
                });
            };
            SubjectItems = new List<AutoCompleteModel>();
        }
        public IList<Voucher> Items { get; set; }
        public IList<AutoCompleteModel> SubjectItems { get; set; }
    }

}
