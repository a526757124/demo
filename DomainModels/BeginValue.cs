using ETVS.Framework.Entity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModels
{
    public class BeginValue : EntityBase<int>
    {
        /// <summary>
        /// 期初金额
        /// </summary>
        public decimal OpenAmount { get; set; }
        /// <summary>
        /// 会计科目
        /// </summary>
        public Subject Subject { get; set; }
        /// <summary>
        /// 公司
        /// </summary>
        public Company Company { get; set; }
    }
}
