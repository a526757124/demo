using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfUI.ViewModel
{
    /// <summary>
    /// 绑定模型类
    /// </summary>
    public class AutoCompleteModel : INotifyPropertyChanged
    {
        public void OnPropertyChanged(string propname)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propname));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private string searchString = string.Empty;
        private string name = string.Empty;
        private string code;
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value;
            this.OnPropertyChanged("Id");
            }
        }
        public string Code
        {
            get { return code; }
            set { code = value;
            this.OnPropertyChanged("Code");
            }
        }
        public string SerchString
        {
            get { return searchString; }


            set
            {
                searchString = value;
                this.OnPropertyChanged("SerchString");
            }
        }


        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                this.OnPropertyChanged("Name");
            }
        }
    }
}
