using Configuration;
using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositories.Configuration
{
    public class VoucherMap : EntityConfigurationBase<Voucher, int>
    {
        public VoucherMap()
        {
            this.Ignore(p => p.CreditTotalAmount);
            this.Ignore(p => p.DebtorTotalAmount);
            this.Property(p => p.VoucherCode).HasMaxLength(20);
            this.HasMany(p => p.VoucherDetails).WithRequired();
            this.HasRequired(p => p.BelongCompany).WithMany();
            this.HasRequired(p => p.CreateUser).WithMany().Map(p => p.MapKey("CreateUser_Id"));
            this.HasRequired(p => p.ModifUser).WithMany().Map(p => p.MapKey("ModifUser_Id"));
            this.HasRequired(p => p.Word).WithMany();
        }
    }
}
