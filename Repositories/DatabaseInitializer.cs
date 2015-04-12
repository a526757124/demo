using DomainModels;
using ETVS.Framework.Entity.Core;
using Framework.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Repositories
{
    public class DatabaseInitializer : DropCreateDatabaseAlways<ETVSContext>
    {
        protected override void Seed(ETVSContext context)
        {
            StringBuilder sql = new StringBuilder();

            Assembly assembly = Assembly.GetAssembly(typeof(User));
            Type baseType = typeof(EntityBase<int>);
            Type[] modelTypes = assembly.GetTypes()
                .Where(type => baseType.IsAssignableFrom(type) && type != baseType && !type.IsAbstract).ToArray();
            //注册实体配置信息
            foreach (Type type in modelTypes)
            {
                sql.AppendFormat(@" ALTER TABLE `etvs`.`{0}` CHANGE COLUMN `RowVersion` `RowVersion` DATETIME NOT NULL 
                                    DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP ; ", type.Name);
            }
            if (sql.Length > 0)
                context.Database.ExecuteSqlCommand(sql.ToString());
            context.Users.Add(new User() { LoginName = "admin", UserName = "管理员", Password = "4QrcOUm6Wau+VuBX8g+IPg==" });
            context.SaveChanges();
            context.SubjectTypes.Add(new SubjectType() { Name = "资产" });
            context.SubjectTypes.Add(new SubjectType() { Name = "负债" });
            context.SubjectTypes.Add(new SubjectType() { Name = "共同" });
            context.SubjectTypes.Add(new SubjectType() { Name = "权益" });
            context.SubjectTypes.Add(new SubjectType() { Name = "成本" });
            context.SubjectTypes.Add(new SubjectType() { Name = "损益" });
            context.SaveChanges();
            context.SubjectCategorys.Add(new SubjectCategory() { Name = "流动资产", BalanceDirection = BalanceDirection.Borrower });
            context.SubjectCategorys.Add(new SubjectCategory() { Name = "长期资产", BalanceDirection = BalanceDirection.Borrower });
            context.SubjectCategorys.Add(new SubjectCategory() { Name = "流动负债", BalanceDirection = BalanceDirection.Lender });
            context.SubjectCategorys.Add(new SubjectCategory() { Name = "长期负债", BalanceDirection = BalanceDirection.Lender });
            context.SubjectCategorys.Add(new SubjectCategory() { Name = "共同", BalanceDirection = BalanceDirection.Borrower });
            context.SubjectCategorys.Add(new SubjectCategory() { Name = "所有者权益", BalanceDirection = BalanceDirection.Lender });
            context.SubjectCategorys.Add(new SubjectCategory() { Name = "成本", BalanceDirection = BalanceDirection.Borrower });
            context.SubjectCategorys.Add(new SubjectCategory() { Name = "营业收入", BalanceDirection = BalanceDirection.Lender });
            context.SubjectCategorys.Add(new SubjectCategory() { Name = "营业成本及税金", BalanceDirection = BalanceDirection.Borrower });
            context.SubjectCategorys.Add(new SubjectCategory() { Name = "期间费用", BalanceDirection = BalanceDirection.Borrower });
            context.SubjectCategorys.Add(new SubjectCategory() { Name = "其他收益", BalanceDirection = BalanceDirection.Lender });
            context.SubjectCategorys.Add(new SubjectCategory() { Name = "其他损失", BalanceDirection = BalanceDirection.Borrower });
            context.SubjectCategorys.Add(new SubjectCategory() { Name = "以前年度损益调整", BalanceDirection = BalanceDirection.Lender });
            context.SubjectCategorys.Add(new SubjectCategory() { Name = "所得税", BalanceDirection = BalanceDirection.Borrower });

            base.Seed(context);
        }
    }
}
