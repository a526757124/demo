using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuerytDtos
{
    public class VoucherQuery
    {
        public DateTime? VoucherDateStart { get; set; }
        public DateTime? VoucherDateEnd { get; set; }
        public string LoginName { get; set; }
    }
}
