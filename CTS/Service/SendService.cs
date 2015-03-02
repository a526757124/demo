using CTS.Context;
using CTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PagedList;
using CTS.Dto;
using CTS.Common;
namespace CTS.Service
{
    public class SendService
    {
        #region 增删改查
        public void Add(Send model)
        {
            using (CTSContext context = new CTSContext())
            {
                if (model.BelongCompany.Id > 0)
                {
                    model.BelongCompany = context.CourierCompanys.FirstOrDefault(p => p.Id == model.BelongCompany.Id);
                }
                else
                {
                    model.BelongCompany = null;
                }
                context.Sends.Add(model);
                context.SaveChanges();
            }
        }
        public void Edit(Send model)
        {
            using (CTSContext context = new CTSContext())
            {
                var send = context.Sends.FirstOrDefault(p => p.Id == model.Id);
                if (model.BelongCompany.Id > 0)
                {
                    send.BelongCompany = context.CourierCompanys.FirstOrDefault(p => p.Id == model.BelongCompany.Id);
                }
                send.CourierNumber = model.CourierNumber;
                send.CustomerAddress = model.CustomerAddress;
                send.CustomerName = model.CustomerName;
                send.CustomerPhone = model.CustomerPhone;
                send.RecipientAddress = model.RecipientAddress;
                send.RecipientName = model.RecipientName;
                send.RecipientPhone = model.RecipientPhone;
                send.Price = model.Price;
                send.Weight = model.Weight;
                send.Remark = model.Remark;
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (CTSContext context = new CTSContext())
            {
                var send = context.Sends.FirstOrDefault(p => p.Id == id);
                send.IsDeleted = true;
                context.SaveChanges();
            }
        }
        public Send GetById(int id)
        {
            using (CTSContext context = new CTSContext())
            {
                var model = context.Sends
                    .Include(p => p.BelongCompany)
                    .FirstOrDefault(p => p.Id == id);
                return model;
            }
        }

        public IPagedList<Send> List(PagedParam<SendQueryDto> queryCond)
        {
            using (CTSContext context = new CTSContext())
            {
                var express = context.Sends
                    .Include(p => p.BelongCompany)
                    .Where(p => !p.IsDeleted);

                if (queryCond.QueryDto != null)
                {
                    if (!string.IsNullOrEmpty(queryCond.QueryDto.CourierNumber))
                    {
                        express = express.Where(p => p.CourierNumber.Equals(queryCond.QueryDto.CourierNumber));
                    }
                    if (!string.IsNullOrEmpty(queryCond.QueryDto.CustomerPhone))
                    {
                        express = express.Where(p => p.CustomerPhone.Equals(queryCond.QueryDto.CustomerPhone) || p.CustomerPhone.Contains(queryCond.QueryDto.CustomerPhone));
                    }
                    if (queryCond.QueryDto.BelongCompanyId.HasValue)
                    {
                        if (queryCond.QueryDto.BelongCompanyId.Value != 0)
                        {
                            express = express.Where(p => p.BelongCompany.Id == queryCond.QueryDto.BelongCompanyId.Value);
                        }
                    }
                    if (queryCond.QueryDto.State.HasValue)
                    {
                        if (queryCond.QueryDto.State != 0)
                        {
                            if (queryCond.QueryDto.State == 1)
                            {
                                express = express.Where(p => !p.IsSendOut);
                            }
                            else
                            {
                                express = express.Where(p => p.IsSendOut);
                            }
                        }
                    }
                    if (queryCond.QueryDto.CreatedTimeStart.HasValue)
                    {
                        express = express.Where(p => p.CreatedTime >= queryCond.QueryDto.CreatedTimeStart.Value);
                    }
                    if (queryCond.QueryDto.CreatedTimeEnd.HasValue)
                    {
                        var dt = queryCond.QueryDto.CreatedTimeEnd.Value.AddDays(1);
                        express = express.Where(p => p.CreatedTime < dt);
                    }
                }
                express = express.OrderByDescending(p => p.CreatedTime);
                var result = express.ToPagedList(queryCond.PageNo, queryCond.PageSize);
                return result;
            }
        }
        #endregion

        public void SendOut(int sendId, string courierNumber, int? courierCompanyId)
        {
            using (CTSContext context = new CTSContext())
            {
                var model = context.Sends
                    .Include(p => p.BelongCompany)
                    .FirstOrDefault(p => p.Id == sendId);
                Customer customer = new Customer();
                customer.CustomerPhone = model.CustomerPhone;
                customer.CustomerName = model.CustomerName;
                customer.CustomerAddress = model.CustomerAddress;
                context.Customers.Add(customer);

                if (!string.IsNullOrEmpty(courierNumber))
                {
                    model.CourierNumber = courierNumber;
                }
                if (courierCompanyId.HasValue)
                {
                    model.BelongCompany = context.CourierCompanys.FirstOrDefault(p => p.Id == courierCompanyId);
                }
                //if (string.IsNullOrEmpty(model.CourierNumber))
                //{
                //    throw new BusinessException("发件时，快递单号不能为空");
                //}
                if (model.BelongCompany == null)
                {
                    throw new BusinessException("发件时，快递公司不能为空");
                }
                model.IsSendOut = true;
                context.SaveChanges();
            }
        }
    }
}