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
using Eniux.Wpf.Controls.WindowCore;
using System.Collections.ObjectModel;
using ThemeManagementModule;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace WpfTest
{
    /// <summary>
    /// Window3.xaml 的交互逻辑
    /// </summary>
    public partial class Window3 : WindowBase
    {
        public Window3()
        {
            InitializeComponent();
     ObservableCollection<Member> memberData = new ObservableCollection<Member>();
     memberData.Add(new Member() { Name = "Joe", Age = "23", Sex = SexOpt.Male, Pass = true, Email = new Uri("mailto:Joe@school.com") });
     memberData.Add(new Member() { Name = "Mike", Age = "20", Sex = SexOpt.Male, Pass = false, Email = new Uri("mailto:Mike@school.com") });
     memberData.Add(new Member() { Name = "Lucy", Age = "25", Sex = SexOpt.Female, Pass = true, Email = new Uri("mailto:Lucy@school.com") });
     memberData.Add(new Member() { Name = "Joe", Age = "23", Sex = SexOpt.Male, Pass = true, Email = new Uri("mailto:Joe@school.com") });
     memberData.Add(new Member() { Name = "Mike", Age = "20", Sex = SexOpt.Male, Pass = false, Email = new Uri("mailto:Mike@school.com") });
     memberData.Add(new Member() { Name = "Lucy", Age = "25", Sex = SexOpt.Female, Pass = true, Email = new Uri("mailto:Lucy@school.com") });
     memberData.Add(new Member() { Name = "Joe", Age = "23", Sex = SexOpt.Male, Pass = true, Email = new Uri("mailto:Joe@school.com") });
     memberData.Add(new Member() { Name = "Mike", Age = "20", Sex = SexOpt.Male, Pass = false, Email = new Uri("mailto:Mike@school.com") });
     memberData.Add(new Member() { Name = "Lucy", Age = "25", Sex = SexOpt.Female, Pass = true, Email = new Uri("mailto:Lucy@school.com") });
     memberData.Add(new Member() { Name = "Joe", Age = "23", Sex = SexOpt.Male, Pass = true, Email = new Uri("mailto:Joe@school.com") });
     memberData.Add(new Member() { Name = "Mike", Age = "20", Sex = SexOpt.Male, Pass = false, Email = new Uri("mailto:Mike@school.com") });
     memberData.Add(new Member() { Name = "Lucy", Age = "25", Sex = SexOpt.Female, Pass = true, Email = new Uri("mailto:Lucy@school.com") });
     memberData.Add(new Member() { Name = "Joe", Age = "23", Sex = SexOpt.Male, Pass = true, Email = new Uri("mailto:Joe@school.com") });
     memberData.Add(new Member() { Name = "Mike", Age = "20", Sex = SexOpt.Male, Pass = false, Email = new Uri("mailto:Mike@school.com") });
     memberData.Add(new Member() { Name = "Lucy", Age = "25", Sex = SexOpt.Female, Pass = true, Email = new Uri("mailto:Lucy@school.com") });
     memberData.Add(new Member() { Name = "Joe", Age = "23", Sex = SexOpt.Male, Pass = true, Email = new Uri("mailto:Joe@school.com") });
     memberData.Add(new Member() { Name = "Mike", Age = "20", Sex = SexOpt.Male, Pass = false, Email = new Uri("mailto:Mike@school.com") });
     memberData.Add(new Member() { Name = "Lucy", Age = "25", Sex = SexOpt.Female, Pass = true, Email = new Uri("mailto:Lucy@school.com") });
     memberData.Add(new Member() { Name = "Joe", Age = "23", Sex = SexOpt.Male, Pass = true, Email = new Uri("mailto:Joe@school.com") });
     memberData.Add(new Member() { Name = "Mike", Age = "20", Sex = SexOpt.Male, Pass = false, Email = new Uri("mailto:Mike@school.com") });
     memberData.Add(new Member() { Name = "Lucy", Age = "25", Sex = SexOpt.Female, Pass = true, Email = new Uri("mailto:Lucy@school.com") });
            memberData.Add(new Member(){  Name = "Joe", Age = "23", Sex = SexOpt.Male,Pass = true, Email = new Uri("mailto:Joe@school.com")});              
            memberData.Add(new Member(){  Name = "Mike", Age = "20",Sex = SexOpt.Male, Pass = false,Email = new Uri("mailto:Mike@school.com")});            
            memberData.Add(new Member(){  Name = "Lucy", Age = "25", Sex = SexOpt.Female, Pass = true, Email = new Uri("mailto:Lucy@school.com")});
            this.DataContext = memberData;
            this.Loaded += new RoutedEventHandler(Window3_Loaded);
        }

        void Window3_Loaded(object sender, RoutedEventArgs e)
        {
            //初始化theme
            ///必加
            InitTheme.LoadConfig();
        }
  
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string str = "在本例中，文档根已被设为一个 FlowDocumentScrollViewer 标记。也就是说，您不再只是定义一个单纯的文档而已。相反，您在定义一个完整的 XAML 界面，而它碰巧使用滚动查看器作为其根。滚动查看器的内容是最开始示例中的流文档。（请注意，命名空间定义现在使用滚动查看器标记，而非流文档标记）。图 9 到图 11 是使用此方法创建的，不同的查看器控件用作根元素。"
+ "我为何把这称为强力方法呢？这是因为，从结构角度看，将用户界面定义与其实际数据相混合会导致一些问题。而更理想的状况是将文档与其界面分开。将读取器与文档混合在一起有点像创建一个 SQL Server™ 表，并出于某种原因定义该表只能在 Windows Forms DataGrid 中显示。有若干方法可让文档与 UI 定义分离。如果想使用上文所示的 ASP.NET 方法将流文档作为 Web 应用程序的一部分显示，您可使用所需的查看器控件定义 ASP.NET 页面，然后只要使用标准 ASP.NET 代码合并到实际内容（单独存储，可能在数据库中）即可。"
+ "另一方面，在一个典型的 WPF 应用程序中，您可以只要使用标准 WPF、Windows 和 XAML 浏览器应用程序 (XBAP) 方法来定义您的用户界面，然后动态加载您的文档即可。图 12 显示的是使用我文章中的一个虚构库的一个简单示例，这些文章显示在左上角的一个列表框中。用户从列表中选择一篇文章时，该文档会自动加载到占用大部分窗体的 Flow Document Reader 控件。请注意，诸如 alpha 值混合处理等标准 WPF 技术在此设置中也能使用。您会注意到，实际的流文档是半透明的，背景中我的照片也在闪烁。另外也请注意，应用程序使用了一个列表框、图像，一个标签和一个 FlowDocumentReader 控件来创建虚构文章的库。" + "Load 方法会返回一个对象，因为 XAML 文件中的根元素可以代表许多不同类型。在我的例子中，我知道返回值为 FlowDocument，因此我只要执行一个转换，并将该文档指定给 FlowDocumentReader 控件的 Document 属性即可（此例中，我将控件实例命名为 documentReader）。请记住，这只是个例子。生产品质的代码此处当然还需要一些错误处理。" + "Load 方法会返回一个对象，因为 XAML 文件中的根元素可以代表许多不同类型。在我的例子中，我知道返回值为 FlowDocument，因此我只要执行一个转换，并将该文档指定给 FlowDocumentReader 控件的 Document 属性即可（此例中，我将控件实例命名为 documentReader）。请记住，这只是个例子。生产品质的代码此处当然还需要一些错误处理。" + "Load 方法会返回一个对象，因为 XAML 文件中的根元素可以代表许多不同类型。在我的例子中，我知道返回值为 FlowDocument，因此我只要执行一个转换，并将该文档指定给 FlowDocumentReader 控件的 Document 属性即可（此例中，我将控件实例命名为 documentReader）。请记住，这只是个例子。生产品质的代码此处当然还需要一些错误处理" + "Load 方法会返回一个对象，因为 XAML 文件中的根元素可以代表许多不同类型。在我的例子中，我知道返回值为 FlowDocument，因此我只要执行一个转换，并将该文档指定给 FlowDocumentReader 控件的 Document 属性即可（此例中，我将控件实例命名为 documentReader）。请记住，这只是个例子。生产品质的代码此处当然还需要一些错误处理。";
            WindowBase.ShowMessageBox("测试", str, MessageBoxImage.None, MessageBoxButton.OKCancel);
            WindowBase.ShowMessageBox("测试", str, MessageBoxImage.Asterisk, MessageBoxButton.OK);
            WindowBase.ShowMessageBox("测试", str.Substring(0,200), MessageBoxImage.Error, MessageBoxButton.YesNoCancel);
            WindowBase.ShowMessageBox("测试", str, MessageBoxImage.Exclamation, MessageBoxButton.YesNo);
            WindowBase.ShowMessageBox("测试", str.Substring(0, 200), MessageBoxImage.Hand, "这是确定");
            WindowBase.ShowMessageBox("测试", str, MessageBoxImage.Information, "这是YES", "这是NO", 1);
            WindowBase.ShowMessageBox("测试", str.Substring(0, 200), MessageBoxImage.Question, "这是YES", "这是NO", "这是取消");
            WindowBase.ShowMessageBox("测试", str, MessageBoxImage.Stop, "这是确定", "这是取消");
        }
  

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ThemeMngView tm = new ThemeMngView();
            tm.ShowInTaskbar = false;
            tm.Show();
        }
    }      
    public enum SexOpt { Male, Female };   
    public class Member     
    {         
        public string Name { get; set; }   
        public string Age { get; set; }   
        public SexOpt Sex { get; set; }      
        public bool Pass { get; set; }  
        public Uri Email { get; set; }    
    }  
}
