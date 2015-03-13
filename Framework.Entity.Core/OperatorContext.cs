using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETVS.Framework.Entity.Core
{
    /// <summary>
    ///  Operator操作者上下文，用于记录操作日志
    /// </summary>
    public class OperatorContext
    {
        /// <summary>
        /// 获取或设置 当前上下文
        /// </summary>
        public static OperatorContext Current
        {
            get
            {
               
                return new OperatorContext();
            }
            set
            {
                
            }
        }
        /// <summary>
        /// 获取 当前操作者
        /// </summary>
        public Operator Operator
        {
            get
            {
                return new Operator();
            }
        }
    }
}
