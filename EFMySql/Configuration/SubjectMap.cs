using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFMySql.Configuration
{
    public class SubjectMap : EntityTypeConfiguration<Subject>
    {
        public SubjectMap()
        {
            this.HasKey(p => p.Id);
            this.Property(p => p.Name).HasMaxLength(20).IsRequired();
            this.Property(p => p.MnemonicCode).HasMaxLength(8).IsUnicode();
            this.Property(p => p.Timestamp).IsConcurrencyToken();
        }
    }
}
