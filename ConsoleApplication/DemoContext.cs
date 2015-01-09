using ConsoleApplication.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    public class DemoContext : DbContext
    {
        static DemoContext()
        {
            Database.SetInitializer<DemoContext>(new DemoInitializer());
        }
        public DemoContext()
            : base("DemoConnectionString")
        {
            Configuration.LazyLoadingEnabled = false;
            this.Database.Log = (m) => Trace.WriteLine(m);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
