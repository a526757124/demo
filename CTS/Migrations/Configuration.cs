namespace CTS.Migrations
{
    using CTS.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<CTS.Context.CTSContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CTS.Context.CTSContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            StringBuilder sql = new StringBuilder();

            Assembly assembly = Assembly.GetExecutingAssembly();
            Type baseType = typeof(EntityBase<int>);
            Type[] modelTypes = assembly.GetTypes()
                .Where(type => baseType.IsAssignableFrom(type) && type != baseType && !type.IsAbstract).ToArray();
            //注册实体配置信息
            foreach (Type type in modelTypes)
            {
                sql.AppendFormat(@" ALTER TABLE `cts`.`{0}` CHANGE COLUMN `RowVersion` `RowVersion` DATETIME NOT NULL 
                                    DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP ; ", type.Name);
            }
            if (sql.Length > 0)
                context.Database.ExecuteSqlCommand(sql.ToString());
        }
    }
}
