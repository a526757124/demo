using CTS.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Data;
using CTS.Models;
namespace CTS.Context
{
    public class CTSContext : DbContext
    {
        public CTSContext()
            : base("name=MySqlConnectionString")
        {
            //Database.SetInitializer<CTSContext>(null);
            //Database.SetInitializer<CTSContext>(new CTSInitializer());
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CourierCompany> CourierCompanys { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Send> Sends { get; set; }
        public DbSet<TakeInfo> TakeInfos { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //Assembly assembly = Assembly.GetExecutingAssembly();
            //Type baseType = typeof(IEntityMapper);
            //Type[] mapperTypes = assembly.GetTypes()
            //    .Where(type => baseType.IsAssignableFrom(type) && type != baseType && !type.IsAbstract).ToArray();
            //ICollection<IEntityMapper> result = mapperTypes.Select(type => Activator.CreateInstance(type) as IEntityMapper).ToList();
            ////注册实体配置信息
            //ICollection<IEntityMapper> entityMappers = result;
            //foreach (IEntityMapper mapper in entityMappers)
            //{
            //    mapper.RegistTo(modelBuilder.Configurations);
            //}
            modelBuilder.Configurations.Add(new CourierCompanyMap());
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new ReceiptMap());
            modelBuilder.Configurations.Add(new SendMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}