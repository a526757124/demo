using ETVS.Framework.Entity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModels
{
    /// <summary>
    /// 凭证字
    /// </summary>
    public class VoucherWord : EntityBase<int>
    {
        /// <summary>
        /// 字
        /// </summary>
        public string Name { get; set; }
    }
}
