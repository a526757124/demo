using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CTS.Dto
{
    public class ReceiptQueryDto
    {
        public string CourierNumber { get; set; }
        public string CustomerPhone { get; set; }
        public int? BelongCompanyId { get; set; }
        public int? State { get; set; }
    }
}