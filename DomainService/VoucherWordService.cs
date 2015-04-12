using DomainModels;
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
    public class VoucherWordService : BaseService<VoucherWord>
    {
        #region 增删改查

        public void Add(VoucherWord model)
        {
            using (ETVSContext context = new ETVSContext())
            {
                context.VoucherWords.Add(model);
                context.SaveChanges();
            }
        }
        public void Update(VoucherWord model)
        {
            using (ETVSContext context = new ETVSContext())
            {
                var words = context.VoucherWords.FirstOrDefault(p => p.Id == model.Id);
                words.Name = model.Name;
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
        public VoucherWord GetById(int Id)
        {
            using (ETVSContext context = new ETVSContext())
            {
                return context.VoucherWords.FirstOrDefault(p => p.Id == Id);
            }
        }
        public dynamic GetComboboxList()
        {
            using (ETVSContext context = new ETVSContext())
            {
                var express = context.VoucherWords
                    .Where(p => !p.IsDeleted);
                return express.OrderByDescending(p => p.CreatedTime).Select(s => new
                {
                    id = s.Id,
                    selected = s.Id == 1,
                    text = s.Name
                }).ToList();
            }
        }
        #endregion
    }
}
