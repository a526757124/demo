using Configuration;
using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositories.Configuration
{
    public class VoucherWordMap : EntityConfigurationBase<VoucherWord, int>
    {
        public VoucherWordMap()
        {
            this.Property(p => p.Name).HasMaxLength(20);
        }
    }
}
