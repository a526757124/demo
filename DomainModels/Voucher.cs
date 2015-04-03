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
        /// <summary>
        /// 凭证明细
        /// </summary>
        public virtual List<VoucherDetail> VoucherDetails { get; set; }
        private decimal _debtorTotalAmount;
        /// <summary>
        /// 借方合计
        /// </summary>
        public decimal DebtorTotalAmount
        {
            get
            {
                return _debtorTotalAmount;
            }
            set
            {
                if (VoucherDetails != null)
                    _debtorTotalAmount = VoucherDetails.Sum(p => p.DebtorAmount);
                _debtorTotalAmount = 0;
            }
        }
        private decimal _creditTotalAmount;
        /// <summary>
        /// 贷方合计
        /// </summary>
        public decimal CreditTotalAmount
        {
            get
            {
                return _creditTotalAmount;
            }
            set
            {
                if (VoucherDetails != null)
                    _creditTotalAmount = VoucherDetails.Sum(p => p.CreditAmount);
                _creditTotalAmount = 0;
            }
        }
        /// <summary>
        /// 附单据数量
        /// </summary>
        public int BillsTotal { get; set; }
        private Company _belongCompany;
        
        /// <summary>
        /// 所属公司
        /// </summary>
        public virtual Company BelongCompany
        {
            get { return _belongCompany; }
            set { _belongCompany = value; }

        }
        /// <summary>
        /// 凭证字
        /// </summary>
        public virtual VoucherWord Word { get; set; }
        /// <summary>
        /// 凭证编号
        /// </summary>
        public string VoucherCode { get; set; }
        /// <summary>
        /// 录入人
        /// </summary>
        public User CreateUser
        {
            get;
            set;
        }
        /// <summary>
        /// 最后一次修改人
        /// </summary>
        public User ModifUser
        {
            get;
            set;
        }
    }
}
