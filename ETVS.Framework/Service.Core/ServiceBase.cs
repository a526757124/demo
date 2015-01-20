

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace ETVS.Framework.Service.Core
{
    /// <summary>
    /// 业务实现基类
    /// </summary>
    public abstract class ServiceBase
    {
        protected ServiceBase(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        /// <summary>
        /// 获取或设置 单元操作对象
        /// </summary>
        protected IUnitOfWork UnitOfWork { get; private set; }
    }
}