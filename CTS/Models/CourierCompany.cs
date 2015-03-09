using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CTS.Models
{
    /// <summary>
    /// 快递公司
    /// </summary>
    public class CourierCompany : EntityBase<int>
    {
        /// <summary>
        /// 快递公司名称
        /// </summary>
        public string CourierName { get; set; }
        /// <summary>
        /// 快递公司编号
        /// </summary>
        public string CourierCode { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// 联系电话 座机
        /// </summary>
        public string ContactPhone { get; set; }
        /// <summary>
        /// 联系人手机
        /// </summary>
        public string ContactMobilePhone { get; set; }
        /// <summary>
        /// 是否发送短信
        /// </summary>
        public bool IsSendSMS { get; set; }
        /// <summary>
        /// 注 只能存在一个
        /// </summary>
        public bool IsDefault { get; set; }
    }
}