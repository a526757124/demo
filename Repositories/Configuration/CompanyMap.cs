using Configuration;
using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositories.Configuration
{
    public class CompanyMap: EntityConfigurationBase<Company, int>
    {
        public CompanyMap()
        {
            this.Property(p => p.CompanyName).HasMaxLength(100);
            this.Property(p => p.CompanyPhone).HasMaxLength(20);
            this.Property(p => p.CompanyContact).HasMaxLength(20);
            this.Property(p => p.CompanyAddress).HasMaxLength(200);
        }
    }
}
