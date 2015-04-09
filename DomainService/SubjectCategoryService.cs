using DomainModels;
using Framework.Service;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainService
{
    public class SubjectCategoryService : BaseService<SubjectCategory>
    {
        public SubjectCategory GetById(int Id)
        {
            using (ETVSContext context = new ETVSContext())
            {
                return context.SubjectCategorys.FirstOrDefault(p => p.Id == Id);
            }
        }
        public dynamic GetComboboxList()
        {
            using (ETVSContext context = new ETVSContext())
            {
                var express = context.SubjectCategorys
                    .Where(p => !p.IsDeleted);
                return express.OrderByDescending(p => p.CreatedTime).Select(s => new
                {
                    id = s.Id,
                    selected = s.Id == 1,
                    tag = s.BalanceDirection,
                    text = s.Name
                }).ToList();
            }
        }
    }
}
