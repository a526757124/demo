using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CTS.Models
{
    /// <summary>
    /// 快递收件
    /// </summary>
    public class Receipt : EntityBase<int>
    {
        /// <summary>
        /// 快递单号
        /// </summary>
        public string CourierNumber { get; set; }
        /// <summary>
        /// 所属快递公司
        /// </summary>
        public CourierCompany BelongCompany { get; set; } 
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
        /// 取件信息
        /// </summary>
        public TakeInfo TakeInfo { get; set; }
    }
}