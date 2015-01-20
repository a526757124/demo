using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ETVS.Utility.Logging;
using ETVS.Utility.Exceptions;

using ETVS.Framework.Entity.Core;
using ETVS.Framework.Extensions;
namespace ETVS.Framework
{
    /// <summary>
    /// EntityFramework-CodeFirst数据上下文
    /// </summary>
    public partial class CodeFirstDbContext : DbContext, IUnitOfWork, IDependency
    {
        private static readonly Logger Logger = LogManager.GetLogger(typeof(CodeFirstDbContext));

        public CodeFirstDbContext()
            : base(GetConnectionStringName())
        { }

        /// <summary>
        /// 获取或设置 是否开启事务提交
        /// </summary>
        public bool TransactionEnabled { get; set; }

        /// <summary>
        /// 获取 数据库连接串名称
        /// </summary>
        /// <returns></returns>
        private static string GetConnectionStringName()
        {
            string name = ConfigurationManager.AppSettings.Get("ETVS-ConnectionStringName")
                ?? ConfigurationManager.AppSettings.Get("ConnectionStringName") ?? "default";
            return name;
        }

        /// <summary>
        /// 提交当前单元操作的更改。
        /// </summary>
        /// <returns>操作影响的行数</returns>
        public override int SaveChanges()
        {
            return SaveChanges(true);
        }

        /// <summary>
        /// 提交当前单元操作的更改。
        /// </summary>
        /// <param name="validateOnSaveEnabled">提交保存时是否验证实体约束有效性。</param>
        /// <returns>操作影响的行数</returns>
        internal int SaveChanges(bool validateOnSaveEnabled)
        {
            bool isReturn = Configuration.ValidateOnSaveEnabled != validateOnSaveEnabled;
            try
            {
                Configuration.ValidateOnSaveEnabled = validateOnSaveEnabled;
                //记录实体操作日志
                List<OperatingLog> logs = this.GetEntityOperateLogs().ToList();
                int count = base.SaveChanges();
                if (count > 0)
                {
                    Logger.Info(logs);
                }
                TransactionEnabled = false;
                return count;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null && e.InnerException.InnerException is SqlException)
                {
                    SqlException sqlEx = e.InnerException.InnerException as SqlException;
                    string msg = DataHelper.GetSqlExceptionMessage(sqlEx.Number);
                    throw new ETVSException("提交数据更新时发生异常：" + msg, sqlEx);
                }
                throw;
            }
            finally
            {
                if (isReturn)
                {
                    Configuration.ValidateOnSaveEnabled = !validateOnSaveEnabled;
                }
            }
        }
#if NET45

        #region Overrides of DbContext

        /// <summary>
        /// 异步提交当前单元操作的更改。
        /// </summary>
        /// <returns>操作影响的行数</returns>
        public override Task<int> SaveChangesAsync()
        {
            return SaveChangesAsync(true);
        }

        #endregion

        /// <summary>
        /// 提交当前单元操作的更改。
        /// </summary>
        /// <param name="validateOnSaveEnabled">提交保存时是否验证实体约束有效性。</param>
        /// <returns>操作影响的行数</returns>
        internal async Task<int> SaveChangesAsync(bool validateOnSaveEnabled)
        {
            bool isReturn = Configuration.ValidateOnSaveEnabled != validateOnSaveEnabled;
            try
            {
                Configuration.ValidateOnSaveEnabled = validateOnSaveEnabled;
                //记录实体操作日志
                List<OperatingLog> logs = (await this.GetEntityOperateLogsAsync()).ToList();
                int count = await base.SaveChangesAsync();
                if (count > 0)
                {
                    Logger.Info(logs);
                }
                TransactionEnabled = false;
                return count;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null && e.InnerException.InnerException is SqlException)
                {
                    SqlException sqlEx = e.InnerException.InnerException as SqlException;
                    string msg = DataHelper.GetSqlExceptionMessage(sqlEx.Number);
                    throw new OSharpException("提交数据更新时发生异常：" + msg, sqlEx);
                }
                throw;
            }
            finally
            {
                if (isReturn)
                {
                    Configuration.ValidateOnSaveEnabled = !validateOnSaveEnabled;
                }
            }
        }
#endif

        public DbSet<OperatingLog> OperatingLogs { get; set; }
        public DbSet<OperatingLogItem> OperatingLogItems { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //移除一对多的级联删除
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //注册实体配置信息
            ICollection<IEntityMapper> entityMappers = DatabaseInitializer.EntityMappers;
            foreach (IEntityMapper mapper in entityMappers)
            {
                mapper.RegistTo(modelBuilder.Configurations);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}