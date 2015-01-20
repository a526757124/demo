using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CTS.Models
{
    public class Customer : EntityBase<int>
    {
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
        /// 客户常用快递公司
        /// </summary>
        public CourierCompany CustomerOftenCompany { get; set; }
    }
}