using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFMySql.Configuration
{
    public class SubjectTypeMap : EntityTypeConfiguration<SubjectType>
    {
        public SubjectTypeMap()
        {
            this.HasKey(p => p.Id);
            this.Property(p => p.Name).HasMaxLength(12).IsRequired().IsUnicode();
            this.Property(p => p.Timestamp).IsConcurrencyToken();
        }
    }
}
