using CTS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace CTS.Context
{
    public class CTSInitializer : CreateDatabaseIfNotExists<CTSContext>
    {
        public CTSInitializer()
        {
            
        }
        protected override void Seed(CTSContext context)
        {
            //context.CourierCompanys.Add(new Models.CourierCompany()
            //{
            //    CourierName = "圆通快递",
            //    ContactMobilePhone = "13100001111",
            //    ContactName = "赵女士",
            //    ContactPhone = "028-65413548"
            //});
            //context.SaveChanges();
            //初始化数据库
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