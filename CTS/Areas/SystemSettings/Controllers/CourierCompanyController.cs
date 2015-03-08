using CTS.Context;
using CTS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using CTS.Common;
namespace CTS.Areas.SystemSettings.Controllers
{
    public class CourierCompanyController : BaseController
    {
        //
        // GET: /SystemSettings/CourierCompany/

        public ActionResult Index()
        {
            return View();
        }

        #region 增删改查
        [HttpPost]
        public ActionResult Add(CourierCompany model)
        {
            using (CTSContext context = new CTSContext())
            {
                context.CourierCompanys.Add(model);
                context.SaveChanges();

                return Json(new AjaxResult("添加成功", AjaxResultType.Success));
            }
        }
        public ActionResult Delete(int id)
        {
            using (CTSContext context = new CTSContext())
            {
                context.CourierCompanys.FirstOrDefault(p => p.Id == id).IsDeleted = true;

                context.SaveChanges();

                return Json(new AjaxResult("删除成功", AjaxResultType.Success));
            }
        }
        public ActionResult Edit(CourierCompany model)
        {
            using (CTSContext context = new CTSContext())
            {
                var courierCompany = context.CourierCompanys.FirstOrDefault(p => p.Id == model.Id);
                courierCompany.ContactMobilePhone = model.ContactMobilePhone;
                courierCompany.ContactName = model.ContactName;
                courierCompany.ContactPhone = model.ContactPhone;
                courierCompany.CourierCode = model.CourierCode;
                courierCompany.CourierName = model.CourierName;
                courierCompany.Remark = model.Remark;
                context.SaveChanges();
                return Json(new AjaxResult("编辑成功", AjaxResultType.Success));
            }
        }
        public ActionResult GetById(int id)
        {
            using (CTSContext context = new CTSContext())
            {
                var model = context.CourierCompanys
                    .FirstOrDefault(p => p.Id == id);
                return Json(new AjaxResult("查询成功", AjaxResultType.Success, model));
            }
        }
        public ActionResult List(PagedParam<dynamic> queryCond)
        {
            using (CTSContext context = new CTSContext())
            {
                var result = context.CourierCompanys
                    .Where(p => !p.IsDeleted)
                    .OrderByDescending(p => p.CreatedTime).ToPagedList(queryCond.PageNo, queryCond.PageSize);
                return Json(new AjaxResult("查询成功", AjaxResultType.Success, new { rows = result.ToList(), total = result.TotalItemCount }));
            }
        }

        #endregion


    }
}
