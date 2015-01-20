using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameworkEnum;

namespace Model
{
    /// <summary>
    /// 会计科目
    /// </summary>
    public class Subject : BaseModel
    {

        private string _code;
        /// <summary>
        /// 科目代码
        /// </summary>
        public string Code
        {
            get { return _code; }
            set
            {
                _code = value;
                OnPropertyChanged("Code");
            }
        }
        private string _mnemonicCode;
        /// <summary>
        /// 助记码
        /// </summary>
        public string MnemonicCode
        {
            get { return _mnemonicCode; }
            set
            {
                _mnemonicCode = value;
                OnPropertyChanged("MnemonicCode");
            }
        }
        private string _name;
        /// <summary>
        /// 科目名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        private SubjectType _type;
        /// <summary>
        /// 科目类别
        /// </summary>
        public SubjectType Type
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged("Type");
            }
        }
        private BalanceDirection balanceDirection;
        /// <summary>
        /// 余额方向
        /// </summary>
        public BalanceDirection BalanceDirection
        {
            get { return balanceDirection; }
            set
            {
                balanceDirection = value;
                OnPropertyChanged("BalanceDirection");
            }
        }

    }
}
