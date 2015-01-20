

using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ETVS.Framework.Migrations
{
    public class MigrationsConfiguration : DbMigrationsConfiguration<CodeFirstDbContext>
    {
        static MigrationsConfiguration()
        {
            SeedActions = new List<ISeedAction>();
        }

        public MigrationsConfiguration()
        {
            //是否自动迁移
            AutomaticMigrationsEnabled = false;
            //是否允许数据丢失
            AutomaticMigrationDataLossAllowed = false;
        }

        /// <summary>
        /// 获取 数据迁移初始化种子数据操作信息集合，各个模块可以添加自己的数据初始化操作
        /// </summary>
        public static ICollection<ISeedAction> SeedActions { get; private set; }

        protected override void Seed(CodeFirstDbContext context)
        {
            IEnumerable<ISeedAction> seedActions = SeedActions.OrderBy(m => m.Order);
            foreach (ISeedAction seedAction in seedActions)
            {
                seedAction.Action(context);
            }
        }
    }
}