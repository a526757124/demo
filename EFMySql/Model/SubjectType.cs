﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 科目类别
    /// </summary>
    public class SubjectType : BaseModel
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                base.OnPropertyChanged("Name");
            }
        }
        private string _remark;
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; OnPropertyChanged("Remark"); }

        }
    }
}
