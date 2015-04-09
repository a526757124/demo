using Configuration;
using DomainModels;
using ETVS.Framework.Entity.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Repositories
{
    public class ETVSContext : DbContext, IDisposable
    {
        public ETVSContext()
            : base("MySqlConnectionString")
        {
            //Configuration.LazyLoadingEnabled = false;
            //Configuration.ProxyCreationEnabled = false;
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companys { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SubjectCategory> SubjectCategorys { get; set; }
        public DbSet<SubjectType> SubjectTypes { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<VoucherWord> VoucherWords { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type baseType = typeof(IEntityMapper);
            Type[] mapperTypes = assembly.GetTypes()
                .Where(type => baseType.IsAssignableFrom(type) && type != baseType && !type.IsAbstract).ToArray();
            ICollection<IEntityMapper> result = mapperTypes.Select(type => Activator.CreateInstance(type) as IEntityMapper).ToList();
            //注册实体配置信息
            ICollection<IEntityMapper> entityMappers = result;
            foreach (IEntityMapper mapper in entityMappers)
            {
                mapper.RegistTo(modelBuilder.Configurations);
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
