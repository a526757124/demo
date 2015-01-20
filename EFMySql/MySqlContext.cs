using EFMySql.Configuration;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace EFMySql
{
    public class MySqlContext : DbContext  
    {
        public MySqlContext()
            : base("name=MySqlContext")  
        {  
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new SubjectTypeMap());
            modelBuilder.Configurations.Add(new SubjectMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
