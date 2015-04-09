using DomainModels;
using Framework.Service;
using PagedList;
using QuerytDtos;
using Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Utility.Paged;
using Framework.Enums;

namespace DomainService
{
    public class SubjectService : BaseService<Subject>
    {
        #region 增删改查

        public void Add(Subject model)
        {
            using (ETVSContext context = new ETVSContext())
            {
                model.Category = context.SubjectCategorys.FirstOrDefault(p => p.Id == model.Category.Id);
                model.ParentSubject = model.ParentSubject == null ? null : context.Subjects.FirstOrDefault(p => p.Id == model.ParentSubject.Id);
                model.Type = context.SubjectTypes.FirstOrDefault(p => p.Id == model.Type.Id);
                context.Subjects.Add(model);
                context.SaveChanges();
            }
        }
        public void Update(Subject model)
        {
            using (ETVSContext context = new ETVSContext())
            {
                var subject = context.Subjects.FirstOrDefault(p => p.Id == model.Id);
                subject.Category = context.SubjectCategorys.FirstOrDefault(p => p.Id == model.Category.Id);
                subject.ParentSubject = model.ParentSubject == null ? null : context.Subjects.FirstOrDefault(p => p.Id == model.ParentSubject.Id);
                subject.Type = context.SubjectTypes.FirstOrDefault(p => p.Id == model.Type.Id);
                subject.Name = model.Name;
                subject.MnemonicCode = model.MnemonicCode;
                subject.BalanceDirection = model.BalanceDirection;
                subject.Code = model.Code;
                context.SaveChanges();
            }
        }
        public void Delete(int Id)
        {
            using (ETVSContext context = new ETVSContext())
            {
                context.SaveChanges();
            }
        }
        public Subject GetById(int Id)
        {
            using (ETVSContext context = new ETVSContext())
            {
                return context.Subjects
                    .Include(p=>p.ParentSubject)
                    .Include(p=>p.Category)
                    .FirstOrDefault(p => p.Id == Id);
            }
        }
        public dynamic GetTreeGridList(int? type)
        {
            type = type.HasValue ? type.Value : 0;
            using (ETVSContext context = new ETVSContext())
            {
                var express = context.Subjects
                    .Include(p => p.ParentSubject)
                    .Include(p => p.Type)
                    .Where(p => !p.IsDeleted && p.Type.Id == type);
                return express.OrderBy(p => p.Id).Select(s => new
                    {
                        s.Id,
                        s.Code,
                        s.MnemonicCode,
                        s.Name,
                        _parentId =s.ParentSubject==null?0: s.ParentSubject.Id,
                        s.BalanceDirection,
                        CategoryName = s.Category.Name
                    }).ToList();
            }
        }
        public IPagedList<Subject> List(PagedParam<SubjectQuery> queryCond)
        {
            using (ETVSContext context = new ETVSContext())
            {
                var express = context.Subjects
                        .Where(p => !p.IsDeleted);
                return express.OrderByDescending(p => p.CreatedTime).ToPagedList(queryCond.PageNo, queryCond.PageSize);
            }
        }
        #endregion
    }
}
