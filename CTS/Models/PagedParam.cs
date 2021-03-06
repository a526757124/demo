﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CTS.Models
{
    public class PagedParam<T> where T : class
    {
        public PagedParam()
        {

        }
        public PagedParam(int pageNo, int pageSize, dynamic queryDto)
        {
            this.PageNo = pageNo;
            this.PageSize = pageSize;
            this.QueryDto = queryDto;
        }

        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public T QueryDto { get; set; }
    }
}
