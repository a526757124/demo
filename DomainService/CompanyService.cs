using DomainModels;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainService
{
    public class CompanyService
    {
        public void Add(Company model)
        {
            using (ETVSContext context = new ETVSContext())
            {
                
            }
        }
        public void Update(Company model)
        {

        }
        public void Delete(int Id)
        {

        }
        public List<Company> GetList()
        {
            return null;
        }
    }
}
