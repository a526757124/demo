using ETVS.DomainService;
using ETVS.DomainModels;
using ETVS.Utility.Data;
using ETVS.Framework.Entity.Core;
using ETVS.Repositories.Configuration;
using ETVS.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using System.Reflection;
using ETVS.Repositories;


namespace ConsoleETVS
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly assembly = Assembly.GetAssembly(typeof(SubjectTypeMap));
            DatabaseInitializer.AddMapperAssembly(assembly);

            
            using (ETVS.Repositories.ETVSContext etv = new ETVS.Repositories.ETVSContext())
            {
                //IRepository<SubjectType, int> re = new Repository<SubjectType, int>(re.UnitOfWork);
                //etv.TransactionEnabled = true;
                var model = new SubjectType
                {
                    Name = "abc"
                };
                etv.SubjectTypes.Add(model);
                
                etv.SaveChanges();

            }

            //Assembly assembly = Assembly.GetExecutingAssembly();
            //assembly = Assembly.GetAssembly(typeof(SubjectTypeMap));
            //DatabaseInitializer.AddMapperAssembly(assembly);
            //var a= DatabaseInitializer.EntityMappers;
            //DatabaseInitializer.Initialize();
        }
    }
}
