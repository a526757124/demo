using ETVS.DomainModels;
using ETVS.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETVS.Repositories.Configuration
{
    public class SubjectTypeMap : EntityConfigurationBase<SubjectType,int>
    {
        public SubjectTypeMap()
        {
            this.HasKey(p => p.Id);
            this.Property(p=>p.Name).HasMaxLength(12).IsRequired();
        }
    }
}
