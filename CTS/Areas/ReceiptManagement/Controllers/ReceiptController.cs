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
        [HttpPost]
        public ActionResult Add(Receipt model)
        {
            using (CTSContext context = new CTSContext())
            {

                model.BelongCompany = context.CourierCompanys.FirstOrDefault();
                context.Receipts.Add(model);
                context.SaveChanges();
                return Json("T");
            }
        }
        public ActionResult Edit(Receipt model)
        {
            using (CTSContext context = new CTSContext())
            {
                var receipts = context.Receipts.FirstOrDefault(p => p.Id == model.Id);
                receipts.BelongCompany = context.CourierCompanys.FirstOrDefault();
                receipts.CourierNumber = model.CourierNumber;
                receipts.CustomerAddress = model.CustomerAddress;
                receipts.CustomerName = model.CustomerPhone;
                receipts.Remark = model.Remark;
                context.SaveChanges();
                return Json("T");
            }
        }
        public ActionResult GetById(int id)
        {
            using (CTSContext context = new CTSContext())
            {
                var model = context.Receipts.FirstOrDefault(p=>p.Id==id);
                return Json(model);
            }
        }
        public ActionResult List(PagedParam queryCond)
        {
            using (CTSContext context = new CTSContext())
            {
                var result = context.Receipts
                    .Include(p=>p.BelongCompany)
                    .OrderByDescending(p => p.CreatedTime).ToPagedList(queryCond.PageNo, queryCond.PageSize);
                return Json(new { rows = result.ToList(), total = result.TotalItemCount });
            }
        }
        
    }
}
