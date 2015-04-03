using Configuration;
using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositories.Configuration
{
    public class SubjectMap : EntityConfigurationBase<Subject, int>
    {
        public SubjectMap()
        {
            this.Property(p => p.Name).HasMaxLength(20);
            this.Property(p => p.MnemonicCode).HasMaxLength(20);
            this.Property(p => p.Code).HasMaxLength(20);
        }
    }
}
