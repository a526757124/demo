using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CTS.Models
{
    public class ResultData
    {
        public bool Status { get; set; }
        public dynamic Result { get; set; }
        public string Message { get; set; }
    }
}