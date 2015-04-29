using DomainModels;
using Framework.Service;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using PagedList;
using Utility.Paged;
using QuerytDtos;
using Utility.Exceptions;
using MFS.Common;

namespace DomainService
{
    public class VoucherService : BaseService<Voucher>
    {

        #region 增删改查
        public void AddUpdate(Voucher model)
        {
            model.VoucherDetails = model.VoucherDetails ?? new List<VoucherDetail>();

            using (ETVSContext context = new ETVSContext())
            {
                if (model.Id <= 0)
                {
                    if (model.VoucherDetails.Count <= 0)
                        throw new BusinessException("请添加凭证明细");
                    model.CreditTotalAmount = model.VoucherDetails.Sum(p => p.CreditAmount);
                    model.DebtorTotalAmount = model.VoucherDetails.Sum(p => p.DebtorAmount);
                    if (model.CreditTotalAmount != model.DebtorTotalAmount)
                    {
                        throw new BusinessException("借贷金额不平衡");
                    }

                    model.BelongCompany = context.Companys.FirstOrDefault(p => p.Id == CurrentCompany.Id);
                    model.CreateUser = context.Users.FirstOrDefault(p => p.Id == CurrentUser.Id);
                    model.Word = context.VoucherWords.FirstOrDefault(p => p.Id == model.Word.Id);
                    model.ModifUser = model.CreateUser;
                    foreach (var item in model.VoucherDetails)
                    {
                        item.Subject = context.Subjects.FirstOrDefault(p => p.Id == item.Subject.Id);
                    }
                    context.Vouchers.Add(model);

                }
                else
                {
                    var temp = context.Vouchers
                        //.Include(p => p.VoucherDetails.Select(s => s.Subject))
                        .FirstOrDefault(p => p.Id == model.Id);
                    temp.ModifUser = context.Users.FirstOrDefault(p => p.Id == CurrentUser.Id);
                    temp.BillsTotal = model.BillsTotal;
                    temp.Remark = model.Remark;
                    temp.VoucherCode = model.VoucherCode;
                    temp.VoucherDate = model.VoucherDate;
                    temp.Word = context.VoucherWords.FirstOrDefault(p => p.Id == model.Word.Id);

                    var VoucherDetails = context.VoucherDetails.Where(p => p.Voucher.Id == temp.Id && !p.IsDeleted).ToList();

                    foreach (var item in model.VoucherDetails)
                    {
                        var dt = VoucherDetails.FirstOrDefault(p => p.Id == item.Id);
                        if (dt == null || dt.Id <= 0)
                        {
                            item.Subject = context.Subjects.Include(p => p.Category).FirstOrDefault(p => p.Id == item.Subject.Id);
                            item.Voucher = temp;
                            VoucherDetails.Add(item);
                        }
                        else
                        {
                            dt.Digest = item.Digest;
                            dt.CreditAmount = item.CreditAmount;
                            dt.DebtorAmount = item.DebtorAmount;
                            dt.Subject = context.Subjects.Include(p => p.Category).FirstOrDefault(p => p.Id == item.Subject.Id);
                        }
                    }
                    foreach (var item in model.VoucherDetails.Where(p => !p.IsDeleted))
                    {
                        if (!model.VoucherDetails.Any(p => p.Id == item.Id))
                        {
                            item.IsDeleted = true;
                        }
                    }
                    if (VoucherDetails.Count(p => !p.IsDeleted) <= 0)
                        throw new BusinessException("请添加凭证明细");
                    temp.CreditTotalAmount = VoucherDetails.Where(p => !p.IsDeleted).Sum(p => p.CreditAmount);
                    temp.DebtorTotalAmount = VoucherDetails.Where(p => !p.IsDeleted).Sum(p => p.DebtorAmount);
                    if (temp.CreditTotalAmount != temp.DebtorTotalAmount)
                    {
                        throw new BusinessException("借贷金额不平衡");
                    }
                    foreach (var item in VoucherDetails)
                    {
                        if (item.Id <= 0)
                        {
                            context.VoucherDetails.Add(item);
                        }
                    }
                }
                
                context.SaveChanges();
            }
        }
        public void Add(Voucher model)
        {
            using (ETVSContext context = new ETVSContext())
            {
                context.Vouchers.Add(model);
                context.SaveChanges();
            }
        }
        public void Update(Voucher model)
        {
            using (ETVSContext context = new ETVSContext())
            {

                context.SaveChanges();
            }
        }
        public void Delete(int Id)
        {
            using (ETVSContext context = new ETVSContext())
            {
                var temp = context.Vouchers
                        .FirstOrDefault(p => p.Id == Id);
                temp.IsDeleted = true;
                context.SaveChanges();
            }
        }
        public Voucher GetById(int Id)
        {
            using (ETVSContext context = new ETVSContext())
            {
                return context.Vouchers
                    //.Include(p => p.VoucherDetails.Select(s => s.Subject))
                    .Include(p => p.Word).FirstOrDefault(p => p.Id == Id);
            }
        }
        public List<VoucherDetail> GetVoucherDetailByVoucherId(int Id)
        {
            using (ETVSContext context = new ETVSContext())
            {
                //var result=  context.Vouchers
                //     .Include(p => p.VoucherDetails.Select(s => s.Subject))
                //     .Include(p => p.Word).FirstOrDefault(p => p.Id == Id);
                //return result.VoucherDetails.Where(p=>!p.IsDeleted).ToList();
                return context.VoucherDetails
                    .Include(p=>p.Subject)
                    .Where(p => !p.IsDeleted && p.Voucher.Id == Id).ToList();
            }
        }
        public IPagedList<Voucher> List(PagedParam<VoucherQuery> queryCond)
        {
            ETVSContext context = new ETVSContext();
            var express = context.Vouchers
                //.Include(p => p.VoucherDetails.Select(s => s.Subject))
                .Include(p => p.CreateUser)
                .Include(p => p.Word)
                .Where(p => !p.IsDeleted && p.BelongCompany.Id == CurrentCompany.Id);
            if (queryCond.QueryDto != null)
            {
                if (queryCond.QueryDto.VoucherDateStart.HasValue)
                {
                    var start = queryCond.QueryDto.VoucherDateStart.Value.Date;
                    express = express.Where(p => p.VoucherDate >= start);
                }
                if (queryCond.QueryDto.VoucherDateStart.HasValue)
                {
                    var end = queryCond.QueryDto.VoucherDateEnd.Value.AddDays(1).Date;
                    express = express.Where(p => p.VoucherDate < end);
                }
                if (!string.IsNullOrEmpty(queryCond.QueryDto.LoginName))
                {
                    express = express.Where(p => p.CreateUser.UserName.Contains(queryCond.QueryDto.LoginName));
                }
            }
            return express.OrderByDescending(p => p.VoucherDate).ToPagedList(queryCond.PageNo, queryCond.PageSize);


        }
        #endregion
    }
}
