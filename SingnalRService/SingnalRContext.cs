using SingnalRService.Configuration;
using SingnalRService.DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingnalRService
{
    public class SingnalRContext : DbContext
    {
        static SingnalRContext()
        {
            Database.SetInitializer<SingnalRContext>(new SingnalRInitializer());
        }
        public SingnalRContext()
            : base("SingnalRConnectionString")
        {
            Configuration.LazyLoadingEnabled = true;
            this.Database.Log = (m) => Trace.WriteLine(m);
        }
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new MessageMap());
            modelBuilder.Configurations.Add(new UserMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
