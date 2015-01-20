
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ETVS.Framework
{
    /// <summary>
    /// 添加DTO
    /// </summary>
    public interface IAddDto
    { }


    /// <summary>
    /// 编辑DTO
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IEditDto<TKey>
    {
        /// <summary>
        /// 获取或设置 主键，唯一标识
        /// </summary>
        TKey Id { get; set; }
    }
}