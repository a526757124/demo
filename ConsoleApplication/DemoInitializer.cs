using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    public class DemoInitializer : CreateDatabaseIfNotExists<DemoContext>
    {
        public DemoInitializer()
        {
            
        }
        protected override void Seed(DemoContext context)
        {
            var dept=new Department { Id = Guid.NewGuid(), Name = "部门" };
            context.Departments.Add(dept);
            context.Users.Add(new User { Id = Guid.NewGuid(), Name = "admin" , Dept=dept, Sex='0'});
            //context.SaveChanges();
            base.Seed(context);
        }
    }
}
