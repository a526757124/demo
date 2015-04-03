using Configuration;
using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositories.Configuration
{
    public class VoucherDetailMap : EntityConfigurationBase<VoucherDetail, int>
    {
        public VoucherDetailMap()
        {
            this.Property(p => p.CreditAmount).HasPrecision(18, 4);
            this.Property(p => p.DebtorAmount).HasPrecision(18, 4);
            this.Property(p => p.Digest).HasMaxLength(200);
            this.HasRequired(p => p.Subject).WithMany();
        }
    }
}
