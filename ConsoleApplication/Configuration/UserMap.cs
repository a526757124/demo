using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication.Configuration
{
    public class UserMap:EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            this.Property(p => p.Name).HasMaxLength(8);
        }
    }
}
