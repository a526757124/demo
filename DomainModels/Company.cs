using ETVS.Framework.Entity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModels
{

    /// <summary>
    /// 公司
    /// </summary>
    public class Company : EntityBase<int>
    {
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 公司联系人
        /// </summary>
        public string CompanyContact { get; set; }
        /// <summary>
        /// 公司联系电话
        /// </summary>
        public string CompanyPhone { get;set;}
        /// <summary>
        /// 公司地址
        /// </summary>
        public string CompanyAddress { get; set; }

    }
}
