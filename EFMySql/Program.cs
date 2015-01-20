using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.Entity;
namespace EFMySql
{
    class Program
    {
        static void Main(string[] args)
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<MySqlContext>());
            
            var context = new ContextEX();
            context.SubjectTypes.Add(new SubjectType()
            {
                Name = "abc1",
            });
            context.SaveChanges();
            
        }
    }
}
