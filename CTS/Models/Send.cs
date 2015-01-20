using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CTS.Models
{

    /// <summary>
    /// 快递发件
    /// </summary>
    public class Send : EntityBase<int>
    {
        /// <summary>
        /// 快递单号
        /// </summary>
        public string CourierNumber { get; set; }
        /// <summary>
        /// 所属快递公司 客户是否指定快递公司如果不指定则使用系统设置中默认设置
        /// </summary>
        public virtual CourierCompany BelongCompany { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 客户电话
        /// </summary>
        public string CustomerPhone { get; set; }
        /// <summary>
        /// 客户地址
        /// </summary>
        public string CustomerAddress { get; set; }
        /// <summary>
        /// 收件人
        /// </summary>
        public string RecipientName { get; set; }
        /// <summary>
        /// 收件人电话
        /// </summary>
        public string RecipientPhone { get; set; }
        /// <summary>
        /// 收件人地址
        /// </summary>
        public string RecipientAddress { get; set; }
    }
}