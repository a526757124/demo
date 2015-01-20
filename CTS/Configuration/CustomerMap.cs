using CTS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace CTS.Configuration
{
    public class CustomerMap : EntityConfigurationBase<Customer, int>
    {
        public CustomerMap()
        {

            this.Property(p => p.CustomerAddress).HasMaxLength(500);
            this.Property(p => p.CustomerName).HasMaxLength(30);
            this.HasOptional(p => p.CustomerOftenCompany).WithMany();
            this.Property(p => p.CustomerPhone).HasMaxLength(12);
        }
    }
}