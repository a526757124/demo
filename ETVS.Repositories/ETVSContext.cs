using ETVS.DomainModels;
using ETVS.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETVS.Repositories
{
    public class ETVSContext:CodeFirstDbContext 
    {
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SubjectType> SubjectTypes { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
    }
}
