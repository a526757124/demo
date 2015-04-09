namespace Repositories.Migrations
{
    using DomainModels;
    using ETVS.Framework.Entity.Core;
    using Framework.Enums;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<Repositories.ETVSContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Repositories.ETVSContext context)
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

            Assembly assembly = Assembly.GetAssembly(typeof(User));
            Type baseType = typeof(EntityBase<int>);
            Type[] modelTypes = assembly.GetTypes()
                .Where(type => baseType.IsAssignableFrom(type) && type != baseType && !type.IsAbstract).ToArray();
            //ע��ʵ��������Ϣ
            foreach (Type type in modelTypes)
            {
                sql.AppendFormat(@" ALTER TABLE `etvs`.`{0}` CHANGE COLUMN `RowVersion` `RowVersion` DATETIME NOT NULL 
                                    DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP ; ", type.Name);
            }
            if (sql.Length > 0)
                context.Database.ExecuteSqlCommand(sql.ToString());
            context.SubjectTypes.Add(new SubjectType() { Name = "�ʲ�" });
            context.SubjectTypes.Add(new SubjectType() { Name = "��ծ" });
            context.SubjectTypes.Add(new SubjectType() { Name = "��ͬ" });
            context.SubjectTypes.Add(new SubjectType() { Name = "Ȩ��" });
            context.SubjectTypes.Add(new SubjectType() { Name = "�ɱ�" });
            context.SubjectTypes.Add(new SubjectType() { Name = "����" });
            context.SaveChanges();
            context.SubjectCategorys.Add(new SubjectCategory() { Name = "�����ʲ�", BalanceDirection = BalanceDirection.Borrower});
            context.SubjectCategorys.Add(new SubjectCategory() { Name = "�����ʲ�", BalanceDirection = BalanceDirection.Borrower });
            context.SubjectCategorys.Add(new SubjectCategory() { Name = "������ծ", BalanceDirection = BalanceDirection.Lender });
            context.SubjectCategorys.Add(new SubjectCategory() { Name = "���ڸ�ծ", BalanceDirection = BalanceDirection.Lender });
            context.SubjectCategorys.Add(new SubjectCategory() { Name = "��ͬ", BalanceDirection = BalanceDirection.Borrower });
            context.SubjectCategorys.Add(new SubjectCategory() { Name = "������Ȩ��", BalanceDirection = BalanceDirection.Lender });
            context.SubjectCategorys.Add(new SubjectCategory() { Name = "�ɱ�", BalanceDirection = BalanceDirection.Borrower });
            context.SubjectCategorys.Add(new SubjectCategory() { Name = "Ӫҵ����", BalanceDirection = BalanceDirection.Lender });
            context.SubjectCategorys.Add(new SubjectCategory() { Name = "Ӫҵ�ɱ���˰��", BalanceDirection = BalanceDirection.Borrower });
            context.SubjectCategorys.Add(new SubjectCategory() { Name = "�ڼ����", BalanceDirection = BalanceDirection.Borrower });
            context.SubjectCategorys.Add(new SubjectCategory() { Name = "��������", BalanceDirection = BalanceDirection.Lender });
            context.SubjectCategorys.Add(new SubjectCategory() { Name = "������ʧ", BalanceDirection = BalanceDirection.Borrower });
            context.SubjectCategorys.Add(new SubjectCategory() { Name = "��ǰ����������", BalanceDirection = BalanceDirection.Lender });
            context.SubjectCategorys.Add(new SubjectCategory() { Name = "����˰", BalanceDirection = BalanceDirection.Borrower });

        }
    }
}
