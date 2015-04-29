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
    public class DatabaseInitializer //: DropCreateDatabaseAlways<ETVSContext>
    {
        public static void Seed(ETVSContext context)
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

            context.VoucherWords.Add(new VoucherWord() { Name = "记" });

            context.Companys.Add(new Company() { CompanyName="管理员", CompanyAddress="xx", CompanyContact="xx", CompanyPhone="xx"});
            context.SaveChanges();
            sql.Clear();
            sql.Append(@"INSERT INTO `Subject`( Code, MnemonicCode, Name, BalanceDirection, Remark, IsDeleted, CreatedTime, RowVersion, Category_Id, ParentSubject_Id, Type_Id) 
VALUES 
('1001','现金','库存现金',0,NULL,0,'2015-04-11 21:28:55','2015-04-11 21:28:47',1,NULL,1),
('1002','存款','银行存款',0,NULL,0,'2015-04-11 21:29:24','2015-04-11 21:29:16',1,NULL,1),
('1003','交易性金融资产','交易性金融资产',0,NULL,0,'2015-04-11 21:29:56','2015-04-11 21:29:47',1,NULL,1),
('1004','应收票据','应收票据',0,NULL,0,'2015-04-11 21:30:27','2015-04-11 21:30:20',1,NULL,1),
('1005','应收账款','应收账款',0,NULL,0,'2015-04-11 21:30:50','2015-04-11 21:30:42',1,NULL,1),
('1006','预付款项','预付款项',0,NULL,0,'2015-04-11 21:31:26','2015-04-11 21:31:18',1,NULL,1),
('1007','应收利息','应收利息',0,NULL,0,'2015-04-11 21:31:54','2015-04-11 21:31:46',1,NULL,1),
('1008','应收股利','应收股利',0,NULL,0,'2015-04-11 21:32:20','2015-04-11 21:32:12',1,NULL,1),
('1009','其他应收款','其他应收款',0,NULL,0,'2015-04-11 21:32:42','2015-04-11 21:32:34',1,NULL,1),
('1010','存货','存货',0,NULL,0,'2015-04-11 21:33:03','2015-04-11 21:32:55',1,NULL,1),
('1011','一年内到期的非流动资产','一年内到期的非流动资产',0,NULL,0,'2015-04-11 21:33:30','2015-04-11 21:33:22',1,NULL,1),
('1012','待摊费用','待摊费用',0,NULL,0,'2015-04-11 22:04:12','2015-04-11 22:04:06',1,NULL,1),
('1013','其他流动资产',' 其他流动资产',0,NULL,0,'2015-04-11 22:04:55','2015-04-11 22:04:47',1,NULL,1),
('1014','可供出售金融资产','可供出售金融资产',0,NULL,0,'2015-04-11 22:05:42','2015-04-11 22:05:46',2,NULL,1),
('1015','持有至到期投资','持有至到期投资',0,NULL,0,'2015-04-11 22:06:15','2015-04-11 22:06:06',2,NULL,1),
('1016','长期应收款','长期应收款',0,NULL,0,'2015-04-11 22:06:47','2015-04-11 22:18:22',2,NULL,1),
('1017','长期股权投资','长期股权投资',0,NULL,0,'2015-04-11 22:07:15','2015-04-11 22:07:07',2,NULL,1),
('1018','长期股权投资','长期股权投资',0,NULL,0,'2015-04-11 22:08:31','2015-04-11 22:08:23',2,NULL,1),
('1019','投资性房地产','投资性房地产',0,NULL,0,'2015-04-11 22:09:02','2015-04-11 22:08:53',2,NULL,1),
('1020','固定资产','固定资产',0,NULL,0,'2015-04-11 22:09:37','2015-04-11 22:09:29',2,NULL,1),
('1021','累计折旧','累计折旧',1,NULL,0,'2015-04-11 22:11:48','2015-04-11 22:11:40',2,NULL,1),
('1022','固定资产净值','固定资产净值',0,NULL,0,'2015-04-11 22:14:21','2015-04-11 22:14:13',2,NULL,1),
('1023','在建工程','在建工程',0,NULL,0,'2015-04-11 22:15:11','2015-04-11 22:15:03',2,NULL,1),
('1024','工程物资','工程物资',0,NULL,0,'2015-04-11 22:15:48','2015-04-11 22:15:40',2,NULL,1),
('1025','固定资产清理','固定资产清理',0,NULL,0,'2015-04-11 22:16:28','2015-04-11 22:16:20',2,NULL,1),
('1026','生产性生物资产','生产性生物资产',0,NULL,0,'2015-04-11 22:17:02','2015-04-11 22:16:54',2,NULL,1),
('1027','无形资产','无形资产',0,NULL,0,'2015-04-11 22:17:42','2015-04-11 22:17:34',2,NULL,1),
('1028','开发支出','开发支出',0,NULL,0,'2015-04-11 22:19:12','2015-04-11 22:19:03',2,NULL,1),
('1029','商誉','商誉',0,NULL,0,'2015-04-11 22:20:00','2015-04-11 22:19:51',2,NULL,1),
('1030','长期待摊费用','长期待摊费用',0,NULL,0,'2015-04-11 22:20:32','2015-04-11 22:20:24',2,NULL,1),
('1031','递延所得税资产','递延所得税资产',0,NULL,0,'2015-04-11 22:21:03','2015-04-11 22:20:55',2,NULL,1),
('2001','短期借款','短期借款',1,NULL,0,'2015-04-11 22:22:59','2015-04-11 22:22:52',3,NULL,2),
('2002','交易性金融负债','交易性金融负债',1,NULL,0,'2015-04-11 22:23:36','2015-04-11 22:23:28',3,NULL,2),
('2003','应付票据','应付票据',1,NULL,0,'2015-04-11 22:24:12','2015-04-11 22:24:04',3,NULL,2),
('2004','应付账款','应付账款',1,NULL,0,'2015-04-11 22:24:43','2015-04-11 22:24:35',3,NULL,2),
('2005','预收款项','预收款项',1,NULL,0,'2015-04-11 22:25:10','2015-04-11 22:25:02',3,NULL,2),
('2006','应付职工薪酬','应付职工薪酬',1,NULL,0,'2015-04-11 22:25:46','2015-04-11 22:25:37',3,NULL,2),
('2007','应交税费','应交税费',1,NULL,0,'2015-04-11 22:26:26','2015-04-11 22:26:17',3,NULL,2),
('2008','应付利息','应付利息',1,NULL,0,'2015-04-11 22:26:59','2015-04-11 22:26:51',3,NULL,2),
('2009','应付股利','应付股利',1,NULL,0,'2015-04-11 22:27:26','2015-04-11 22:27:18',3,NULL,2),
('2010','其他应付款','其他应付款',1,NULL,0,'2015-04-11 22:27:56','2015-04-11 22:27:48',3,NULL,2),
('2011','一年内到期的非流动负债','一年内到期的非流动负债',1,NULL,0,'2015-04-11 22:28:24','2015-04-11 22:28:16',3,NULL,2),
('2012','其他流动负债','其他流动负债',1,NULL,0,'2015-04-11 22:28:49','2015-04-11 22:28:41',3,NULL,2),
('2013','长期借款','长期借款',1,NULL,0,'2015-04-11 22:29:26','2015-04-11 22:29:18',4,NULL,2),
('2014','应付债券','应付债券',1,NULL,0,'2015-04-11 22:29:52','2015-04-11 22:29:44',4,NULL,2),
('2015','长期应付款','长期应付款',1,NULL,0,'2015-04-11 22:30:15','2015-04-11 22:30:07',4,NULL,2),
('2016','专项应付款','专项应付款',1,NULL,0,'2015-04-11 22:30:42','2015-04-11 22:30:34',4,NULL,2),
('2017','预计负债','预计负债',1,NULL,0,'2015-04-11 22:31:14','2015-04-11 22:31:06',4,NULL,2),
('2018','递延所得税负债','递延所得税负债',1,NULL,0,'2015-04-11 22:31:42','2015-04-11 22:31:34',4,NULL,2),
('2018','其他非流动负债','其他非流动负债',1,NULL,0,'2015-04-11 22:32:13','2015-04-11 22:32:05',4,NULL,2),
('3001','实收资本（或股本）','实收资本（或股本）',1,NULL,0,'2015-04-11 22:34:03','2015-04-11 22:33:54',6,NULL,4),
('3002','资本公积','资本公积',1,NULL,0,'2015-04-11 22:34:30','2015-04-11 22:34:22',6,NULL,4),
('3003','盈余公积','盈余公积',1,NULL,0,'2015-04-11 22:35:21','2015-04-11 22:35:13',6,NULL,4),
('3004','未分配利润','未分配利润',1,NULL,0,'2015-04-11 22:35:52','2015-04-11 22:35:44',6,NULL,4),
('4001','营业收入','营业收入',1,NULL,0,'2015-04-11 22:40:55','2015-04-11 22:40:47',8,NULL,6),
('4002','营业成本','营业成本',0,NULL,0,'2015-04-11 22:41:34','2015-04-11 22:41:26',9,NULL,6),
('4003','营业税金及附加','营业税金及附加',0,NULL,0,'2015-04-11 22:42:07','2015-04-11 22:41:58',9,NULL,6),
('4004','销售费用','销售费用',0,NULL,0,'2015-04-11 22:43:04','2015-04-11 22:42:56',10,NULL,6),
('4005','管理费用','管理费用',0,NULL,0,'2015-04-11 22:43:36','2015-04-11 22:43:28',10,NULL,6),
('4006','财务费用','财务费用',0,NULL,0,'2015-04-11 22:44:13','2015-04-11 22:44:05',10,NULL,6),
('4007','资产减值损失','资产减值损失',0,NULL,0,'2015-04-11 22:47:04','2015-04-11 22:46:56',12,NULL,6),
('4008','公允价值变动收益','公允价值变动收益',1,NULL,0,'2015-04-11 22:47:45','2015-04-11 22:47:37',11,NULL,6),
('4009','投资收益','投资收益',1,NULL,0,'2015-04-11 22:48:18','2015-04-11 22:48:10',11,NULL,6),
('4010','营业外收入','营业外收入',1,NULL,0,'2015-04-11 22:50:11','2015-04-11 22:50:03',11,NULL,6),
('4011','营业外支出','营业外支出',0,NULL,0,'2015-04-11 22:50:42','2015-04-11 22:50:34',12,NULL,6),
('4012','所得税费用','所得税费用',0,NULL,0,'2015-04-11 22:51:30','2015-04-11 22:51:22',14,NULL,6),

('4006001','利息收入','利息收入',0,NULL,0,'2015-04-11 22:55:36','2015-04-11 22:55:28',10,60,6),
('4006002','其他','其他',0,NULL,0,'2015-04-11 22:57:09','2015-04-11 22:57:01',10,60,6),
('2007001','应交增值税','应交增值税',1,NULL,0,'2015-04-11 23:00:33','2015-04-11 23:00:25',3,38,2),
('2007003','应交城建税','应交城建税',1,NULL,0,'2015-04-11 23:03:06','2015-04-11 23:04:24',3,38,2),
('2007004','应交教育费附加','应交教育费附加',1,NULL,0,'2015-04-11 23:03:57','2015-04-11 23:04:51',3,38,2),
('2007002','应交营业税','应交营业税',1,NULL,0,'2015-04-11 23:05:38','2015-04-11 23:05:30',3,38,2),
('2007005','应交地方教育费附加','应交地方教育费附加',1,NULL,0,'2015-04-11 23:06:33','2015-04-11 23:06:25',3,38,2),
('2007006','应交企业所得税','应交企业所得税',1,NULL,0,'2015-04-11 23:07:52','2015-04-11 23:07:44',3,38,2),
('2007007','应交个人所得税','应交个人所得税',1,NULL,0,'2015-04-11 23:08:33','2015-04-11 23:08:25',3,38,2),
('2007008','应交房产税','应交房产税',1,NULL,0,'2015-04-11 23:16:15','2015-04-11 23:16:06',3,38,2);");
            if (sql.Length > 0)
                context.Database.ExecuteSqlCommand(sql.ToString());
        }
    }
}
