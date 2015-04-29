using ETVS.Framework.Entity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModels
{
    /// <summary>
    /// 凭证明细
    /// </summary>
    public class VoucherDetail : EntityBase<int>
    {
        /// <summary>
        /// 
        /// </summary>
        public Voucher Voucher { get; set; }
        private string _digest;
        /// <summary>
        /// 摘要
        /// </summary>
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
        /// <summary>
        /// 会计科目
        /// </summary>
        public virtual Subject Subject
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
        /// <summary>
        /// 借方金额
        /// </summary>
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
        /// <summary>
        /// 贷方金额
        /// </summary>
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
    }
}
