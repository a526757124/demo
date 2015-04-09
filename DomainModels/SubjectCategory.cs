using ETVS.Framework.Entity.Core;
using Framework.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModels
{
    /// <summary>
    /// 科目类别
    /// </summary>
    public class SubjectCategory : EntityBase<int>
    {
        /// <summary>
        /// 类别名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 默认方向
        /// </summary>
        public BalanceDirection BalanceDirection { get; set; }
    }
}
