using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfUI.FrameworkEnum
{
    public enum BalanceDirection
    {
        /// <summary>
        /// 借方
        /// </summary>
        [Description("借方")]
        Borrower=0,
        /// <summary>
        /// 贷方
        /// </summary>
        [Description("贷方")]
        Lender=1
    }
}
