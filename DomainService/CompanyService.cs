using DomainModels;
using Framework.Service;
using PagedList;
using QuerytDtos;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility.Paged;

namespace DomainService
{
    public class CompanyService : BaseService<Company>
    {
        #region 增删改查
        
        public void Add(Company model)
        {
            using (ETVSContext context = new ETVSContext())
            {
                context.Companys.Add(model);
                context.SaveChanges();
            }
        }
        public void Update(Company model)
        {
            using (ETVSContext context = new ETVSContext())
            {
                var company = context.Companys.FirstOrDefault(p => p.Id == model.Id);
                company.CompanyName = model.CompanyName;
                company.CompanyContact = model.CompanyContact;
                company.CompanyAddress = model.CompanyAddress;
                company.CompanyPhone = model.CompanyPhone;
                context.SaveChanges();
            }
        }
        public void Delete(int Id)
        {
            using (ETVSContext context = new ETVSContext())
            {
                var company = context.Companys.FirstOrDefault(p => p.Id ==Id);
                company.IsDeleted = true;
                context.SaveChanges();
            }
        }
        public Company GetById(int Id)
        {
            using (ETVSContext context = new ETVSContext())
            {
                return context.Companys.FirstOrDefault(p => p.Id == Id);
            }
        }
        public IPagedList<Company> List(PagedParam<CompanyQuery> queryCond)
        {
            using (ETVSContext context = new ETVSContext())
            {
                var express = context.Companys
                        .Where(p => !p.IsDeleted);
                return express.OrderByDescending(p => p.CreatedTime).ToPagedList(queryCond.PageNo, queryCond.PageSize);
            }
        }
        #endregion

    }
}
