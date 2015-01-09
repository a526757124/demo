using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingnalRService
{
    public class SingnalRInitializer : CreateDatabaseIfNotExists<SingnalRContext>
    {
        public SingnalRInitializer()
        {

        }
        public override void InitializeDatabase(SingnalRContext context)
        {
            base.InitializeDatabase(context);
        }
        protected override void Seed(SingnalRContext context)
        {
            base.Seed(context);
        }
    }
}
