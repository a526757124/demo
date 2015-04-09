using DomainModels;
using DomainService;
using QuerytDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utility.Paged;
using WebUI.Common;

namespace WebUI.Controllers
{
    public class CompanyManagementController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Add(Company model)
        {
            new CompanyService().Add(model);
            return Json(new AjaxResult("添加成功", AjaxResultType.Success, null));
        }
        public ActionResult Edit(Company model)
        {
            new CompanyService().Update(model);
            return Json(new AjaxResult("编辑成功", AjaxResultType.Success, null));
        }
        public ActionResult Delete(int id)
        {
            new CompanyService().Delete(id);
            return Json(new AjaxResult("删除成功", AjaxResultType.Success, null));
        }
        public ActionResult List(PagedParam<CompanyQuery> queryCond)
        {
            var result = new CompanyService().List(queryCond);
            return Json(new AjaxResult("查询成功", AjaxResultType.Success, new { rows = result, total = result.TotalItemCount }));
        }

        public ActionResult GetById(int id)
        {
            var result = new CompanyService().GetById(id);
            return Json(new AjaxResult("查询成功", AjaxResultType.Success, result));
        }
    }
}
