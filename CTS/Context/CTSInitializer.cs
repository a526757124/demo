using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CTS.Context
{
    public class CTSInitializer : CreateDatabaseIfNotExists<CTSContext>
    {
        protected override void Seed(CTSContext context)
        {
            context.CourierCompanys.Add(new Models.CourierCompany()
            {
                CourierName = "圆通快递",
                ContactMobilePhone = "13100001111",
                ContactName = "赵女士",
                ContactPhone = "028-65413548"
            });
            base.Seed(context);
        }
    }
}