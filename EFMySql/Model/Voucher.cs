using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Voucher :BaseModel
    {
        private string _digest;
        public string Digest
        {
            get
            {
                return _digest;
            }
            set
            {
                _digest = value;
                OnPropertyChanged("Digest");
            }

        }
        private Subject _subject;
        public Subject Subject
        {
            get
            {
                return _subject;
            }
            set
            {
                _subject = value;
                OnPropertyChanged("Subject");
            }
        }
        private decimal _debtorAmount;
        public decimal DebtorAmount
        {
            get
            {
                return _debtorAmount;
            }
            set
            {
                _debtorAmount = value;
                OnPropertyChanged("DebtorAmount");
            }
        }
        private decimal _creditAmount;
        public decimal CreditAmount
        {
            get
            {
                return _creditAmount;
            }
            set
            {
                _creditAmount = value;
                OnPropertyChanged("CreditAmount");
            }
        }
        private string   _remark;

        public string Remark
        {
            get { return _remark; }
            set { _remark = value; OnPropertyChanged("Remark"); }
            
        }
        
    }
}
