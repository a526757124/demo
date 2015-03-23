using ETVS.Framework.Entity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    /// <summary>
    /// 凭证
    /// </summary>
    public class Voucher : EntityBase<int>
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
            }
        }
        private string _remark;

        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }

        }
    }
}
