using CTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CTS.Configuration
{
    public class CourierCompanyMap : EntityConfigurationBase<CourierCompany,int>
    {
        public CourierCompanyMap()
        {
            this.Property(p => p.ContactMobilePhone).HasMaxLength(12);
            this.Property(p => p.ContactName).HasMaxLength(30);
            this.Property(p => p.ContactPhone).HasMaxLength(12);
            this.Property(p => p.CourierCode).HasMaxLength(50);
            this.Property(p => p.CourierName).HasMaxLength(200);
        }
    }
}