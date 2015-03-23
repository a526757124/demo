using ETVS.Framework.Entity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    /// <summary>
    /// 科目类别
    /// </summary>
    public class SubjectType : EntityBase<int>
    {
        
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
            }
        }
        private string _remark;
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }

        }
    }
}
