using CTS.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using CTS.Models;
using System.Data.Entity;
using CTS.Dto;
using CTS.Common;
using System.Drawing.Printing;
namespace CTS.Areas.ReceiptManagement.Controllers
{
    public class ReceiptController : BaseController
    {
        //
        // GET: /ReceiptManagement/Receipt/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddOrder()
        {
            return View();
        }

        #region 增删改查
        [HttpPost]
        public ActionResult Add(Receipt model)
        {
            using (CTSContext context = new CTSContext())
            {
                model.BelongCompany = context.CourierCompanys.FirstOrDefault(p => p.Id == model.BelongCompany.Id);
                context.Receipts.Add(model);
                context.SaveChanges();
                return Json(new AjaxResult("添加成功", AjaxResultType.Success));
            }
        }
        public ActionResult Delete(int id)
        {
            using (CTSContext context = new CTSContext())
            {
                context.Receipts.FirstOrDefault(p => p.Id == id).IsDeleted = true; 

                context.SaveChanges();

                return Json(new AjaxResult("删除成功", AjaxResultType.Success));
            }
        }
        public ActionResult Edit(Receipt model)
        {
            using (CTSContext context = new CTSContext())
            {
                var receipts = context.Receipts.FirstOrDefault(p => p.Id == model.Id);
                receipts.BelongCompany = context.CourierCompanys.FirstOrDefault(p => p.Id == model.BelongCompany.Id);
                receipts.CourierNumber = model.CourierNumber;
                receipts.CustomerAddress = model.CustomerAddress;
                receipts.CustomerName = model.CustomerName;
                receipts.CustomerPhone = model.CustomerPhone;
                receipts.Remark = model.Remark;
                context.SaveChanges();
                return Json(new AjaxResult("编辑成功", AjaxResultType.Success));
            }
        }
        public ActionResult GetById(int id)
        {
            using (CTSContext context = new CTSContext())
            {
                var model = context.Receipts
                    .Include(p => p.BelongCompany)
                    .Include(p=>p.TakeInfo)
                    .FirstOrDefault(p => p.Id == id);
                return Json(new AjaxResult("查询成功", AjaxResultType.Success, model));
            }
        }
        public ActionResult List(PagedParam<ReceiptQueryDto> queryCond)
        {
            using (CTSContext context = new CTSContext())
            {


                var express = context.Receipts
                    .Include(p => p.BelongCompany)
                    .Include(p => p.TakeInfo)
                    .Where(p => !p.IsDeleted);

                if (queryCond.QueryDto != null)
                {
                    if (!string.IsNullOrEmpty(queryCond.QueryDto.CourierNumber))
                    {
                        express = express.Where(p => p.CourierNumber.Equals(queryCond.QueryDto.CourierNumber));
                    }
                    if (!string.IsNullOrEmpty(queryCond.QueryDto.CustomerName))
                    {
                        express = express.Where(p => p.CustomerName.Contains(queryCond.QueryDto.CustomerName));
                    }
                    if (!string.IsNullOrEmpty(queryCond.QueryDto.CustomerPhone))
                    {
                        express = express.Where(p => p.CustomerPhone.Equals(queryCond.QueryDto.CustomerPhone)||p.CustomerPhone.Contains(queryCond.QueryDto.CustomerPhone));
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
                                express = express.Where(p => p.TakeInfo == null);
                            }
                            else
                            {
                                express = express.Where(p => p.TakeInfo != null);
                            }
                        }
                    }
                    if (queryCond.QueryDto.CreatedTimeStart.HasValue)
                    {
                        express = express.Where(p => p.CreatedTime >= queryCond.QueryDto.CreatedTimeStart.Value);
                    }
                    if (queryCond.QueryDto.CreatedTimeEnd.HasValue)
                    {
                        var dt=queryCond.QueryDto.CreatedTimeEnd.Value.AddDays(1);
                        express = express.Where(p => p.CreatedTime < dt);
                    }
                }
                express=express.OrderByDescending(p => p.CreatedTime);
                var result = express.ToPagedList(queryCond.PageNo, queryCond.PageSize);
                return Json(new AjaxResult("查询成功", AjaxResultType.Success, new { rows = result.ToList(), total = result.TotalItemCount }));
            }
        }

        #endregion

        #region 取件
        public ActionResult Take(FormCollection from,int[] ids)
        {
            using (CTSContext context = new CTSContext())
            {
                var receipts = context.Receipts
                    .Include(p => p.BelongCompany)
                    .Include(p => p.TakeInfo)
                    .Where(p => ids.Contains(p.Id)).ToList();
                if (receipts.GroupBy(g => g.CustomerPhone).Count() > 1)
                {
                    throw new BusinessException("取件时存在不是同一手机号的快递");
                }
                foreach (var item in receipts)
                {
                    item.TakeInfo = new TakeInfo();
                    Customer customer = new Customer();
                    customer.CustomerPhone = item.CustomerPhone;
                    customer.CustomerName = item.CustomerName;
                    customer.CustomerAddress = item.CustomerAddress;
                    context.Customers.Add(customer);
                }
                context.SaveChanges();
                new Printer().Print(receipts);
                return Json(new AjaxResult("取件并打印成功", AjaxResultType.Success));
            }
        }
        #endregion
        #region 打印
        
        public ActionResult Print(int[] ids)
        {
            using (CTSContext context = new CTSContext())
            {
                var list = context.Receipts
                    .Include(p => p.BelongCompany)
                    .Include(p => p.TakeInfo)
                    .Where(p => ids.Contains(p.Id)).ToList();
                if (list.GroupBy(g => g.CustomerPhone).Count() > 1)
                {
                    throw new BusinessException("打印快递单中存在不是同一手机号的快递");
                }
                new Printer().Print(list);
            }
            return Json(new AjaxResult("打印成功", AjaxResultType.Success));
        }
        #endregion
        public ActionResult GetCourierCompanyList(int? tag)
        {
            using (CTSContext context = new CTSContext())
            {

                var result = context.CourierCompanys
                    .Where(p => !p.IsDeleted)
                    .Select(s => new ComboData
                    {
                        id = s.Id,
                        text = s.CourierName
                    }).ToList();
                if (tag != null)
                {
                    result.Add(new ComboData
                    {
                        id = 0,
                        text = "请选择"
                    });
                }
                return Json(result.OrderBy(o=>o.id),JsonRequestBehavior.AllowGet);
            }
        }
    }
    public class ComboData
    {
        public int id { get; set; }
        public string text { get; set; }
    }
}
