using CTS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace CTS.Configuration
{
    public class SendMap : EntityConfigurationBase<Send, int>
    {
        public SendMap()
        {
           
            this.HasOptional(p => p.BelongCompany);
            this.Property(p => p.CourierNumber).HasMaxLength(20);
            this.Property(p => p.CustomerAddress).HasMaxLength(200);
            this.Property(p => p.CustomerName).IsRequired().HasMaxLength(30);
            this.Property(p => p.CustomerPhone).IsRequired().HasMaxLength(12);
            this.Property(p => p.RecipientAddress).IsRequired().HasMaxLength(200);
            this.Property(p => p.RecipientName).IsRequired().HasMaxLength(30);
            this.Property(p => p.RecipientPhone).IsRequired().HasMaxLength(12);

            this.Property(p => p.Weight).HasPrecision(12,4);
            this.Property(p => p.Price).HasPrecision(12, 2);
            this.Property(p => p.CostAmount).HasPrecision(12, 2);
        }
    }
}