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
    public class SubjectTypeService : BaseService<SubjectType>
    {
        #region 增删改查

        public void Add(SubjectType model)
        {
            using (ETVSContext context = new ETVSContext())
            {
            }
        }
        public void Update(SubjectType model)
        {
            using (ETVSContext context = new ETVSContext())
            {
            }
        }
        public void Delete(int Id)
        {
            using (ETVSContext context = new ETVSContext())
            {
                context.SaveChanges();
            }
        }
        public SubjectType GetById(int Id)
        {
            using (ETVSContext context = new ETVSContext())
            {
                return context.SubjectTypes.FirstOrDefault(p => p.Id == Id);
            }
        }
        public dynamic GetTreeList()
        {
            using (ETVSContext context = new ETVSContext())
            {
                var express = context.SubjectTypes
                        .Where(p => !p.IsDeleted);
                return express.Select(s => new
                {
                    id = s.Id,
                    name = s.Name,
                    pId = 0
                }).ToList();
            }
        }
        
        public IPagedList<SubjectType> List(PagedParam<SubjectTypeQuery> queryCond)
        {
            using (ETVSContext context = new ETVSContext())
            {
                var express = context.SubjectTypes
                        .Where(p => !p.IsDeleted);
                return express.OrderByDescending(p => p.CreatedTime).ToPagedList(queryCond.PageNo, queryCond.PageSize);
            }
        }
        #endregion
    }
}
