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

namespace WebUI.Areas.VoucherManagement.Controllers
{
    public class VoucherController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddUpdate(Voucher model)
        {
            new VoucherService().AddUpdate(model);
            return Json(new AjaxResult("保存成功", AjaxResultType.Success, null));
        }
        public ActionResult Delete(int id)
        {
            new VoucherService().Delete(id);
            return Json(new AjaxResult("删除成功", AjaxResultType.Success, null));
        }
        public ActionResult GetVoucherById(int id)
        {
            var result= new VoucherService().GetById(id);
            return Json(new AjaxResult("查询成功", AjaxResultType.Success, result));
        }
        public ActionResult GetVoucherDetailsList(int id)
        {
            var result = new VoucherService().GetVoucherDetailByVoucherId(id);
            return Json(new AjaxResult("查询成功", AjaxResultType.Success, new { rows = result, total = result.Count }));
        }
        public ActionResult GetVoucherWordComboboxList()
        {
            var result=new VoucherWordService().GetComboboxList();
            return Json(new AjaxResult("查询成功", AjaxResultType.Success, result));
        }
        public ActionResult GetSubjectComboboxList(string query)
        {
            var result = new SubjectService().GetComboboxList(query);
            return Json(result);
        }
        [HttpPost]
        public ActionResult List(PagedParam<VoucherQuery> queryCond)
        {
            var result = new VoucherService().List(queryCond);
            return Json(new AjaxResult("查询成功", AjaxResultType.Success, new { rows = result.ToList(), total = result.TotalItemCount }));
        }
    }
}
