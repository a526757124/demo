using CTS.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using CTS.Models;

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
                model.BelongCompany = context.CourierCompanys.FirstOrDefault(p=>p.Id.Equals(model.BelongCompany));
                context.Receipts.Add(model);
                context.SaveChanges();
                return Json("");
            }
        }

        public ActionResult List(PagedParam queryCond)
        {
            using (CTSContext context = new CTSContext())
            {
                var result = context.Receipts.OrderByDescending(p => p.CreatedTime).ToPagedList(queryCond.PageNo, queryCond.PageSize);
                return Json(new { rows = result.ToList(), total = result.TotalItemCount });
            }
        }
    }
}
