using Configuration;
using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositories.Configuration
{
    public class UserMap : EntityConfigurationBase<User, int>
    {
        public UserMap()
        {
            this.Property(p => p.UserName).HasMaxLength(20);
            this.Property(p => p.LoginName).HasMaxLength(20);
            this.Property(p => p.Password).HasMaxLength(32);
        }
    }
}
