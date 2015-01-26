using CTS.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using CTS.Models;
using System.Data.Entity;
namespace CTS.Areas.ReceiptManagement.Controllers
{
    public class ReceiptController : Controller
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

                return Json("T");
            }
        }
        public ActionResult Delete(int id)
        {
            using (CTSContext context = new CTSContext())
            {
                context.Receipts.FirstOrDefault(p => p.Id == id).IsDeleted = true; 

                context.SaveChanges();

                return Json("T");
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
                return Json("T");
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
                return Json(model);
            }
        }
        public ActionResult List(PagedParam queryCond)
        {
            using (CTSContext context = new CTSContext())
            {
                var result = context.Receipts
                    .Include(p => p.BelongCompany)
                    .Include(p => p.TakeInfo)
                    .Where(p => !p.IsDeleted)
                    .OrderByDescending(p => p.CreatedTime).ToPagedList(queryCond.PageNo, queryCond.PageSize);
                return Json(new { rows = result.ToList(), total = result.TotalItemCount });
            }
        }

        #endregion
        #region 取件
        public ActionResult Take(FormCollection from,int? Id)
        {
            using (CTSContext context = new CTSContext())
            {
                var receipts = context.Receipts.FirstOrDefault(p => p.Id == Id);
                receipts.TakeInfo = new TakeInfo();
                context.SaveChanges();
                return Json("T");
            }
        }
        #endregion
        public ActionResult GetCourierCompanyList()
        {
            using (CTSContext context = new CTSContext())
            {
                var result = context.CourierCompanys
                    .Where(p => !p.IsDeleted)
                    .Select(s => new
                    {
                        id = s.Id,
                        text = s.CourierName
                    }).ToList();
                return Json(result);
            }
        }
    }
}
