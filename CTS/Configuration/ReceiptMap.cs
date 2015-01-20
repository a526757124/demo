using CTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CTS.Configuration
{
    public class ReceiptMap:EntityConfigurationBase<Receipt,int>
    {
        public ReceiptMap()
        {
            this.HasRequired(p => p.BelongCompany).WithMany();
            this.Property(p => p.CourierNumber).IsRequired().HasMaxLength(20);
            this.Property(p => p.CustomerAddress).HasMaxLength(500);
            this.Property(p => p.CustomerName).HasMaxLength(30);
            this.Property(p => p.CustomerPhone).HasMaxLength(12);
        }
    }
}