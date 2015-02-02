using CTS.Context;
using CTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CTS.Service
{
    public class CustomerService
    {
        public void Add(Customer model)
        {
            using (CTSContext context = new CTSContext())
            {
                if (!context.Customers.Any(p => p.CustomerName.Equals(model.CustomerName) && p.CustomerPhone.Equals(model.CustomerPhone)))
                {
                    context.Customers.Add(model);
                    context.SaveChanges();
                }
            }
        }
    }
}