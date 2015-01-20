using CTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CTS.Configuration
{
    public class SendMap : EntityConfigurationBase<Send, int>
    {
        public SendMap()
        {
            this.HasRequired(p => p.BelongCompany).WithOptional();
            this.Property(p => p.CourierNumber).IsRequired().HasMaxLength(100);
            this.Property(p => p.CustomerAddress).HasMaxLength(500);
            this.Property(p => p.CustomerName).IsRequired().HasMaxLength(30);
            this.Property(p => p.CustomerPhone).IsRequired().HasMaxLength(12);
            this.Property(p => p.RecipientAddress).IsRequired().HasMaxLength(500);
            this.Property(p => p.RecipientName).IsRequired().HasMaxLength(30);
            this.Property(p => p.RecipientPhone).IsRequired().HasMaxLength(12);
        }
    }
}