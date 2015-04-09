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
    public class UserManagementController : BaseController
    {
        //
        // GET: /UserManagement/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Add(User model)
        {
            new UserService().Add(model);
            return Json(new AjaxResult("添加成功", AjaxResultType.Success, null));
        }
        public ActionResult Edit(User model)
        {
            new UserService().Update(model);
            return Json(new AjaxResult("修改成功", AjaxResultType.Success, null));
        }
        public ActionResult Delete(int id)
        {
            new UserService().Delete(id);
            return Json(new AjaxResult("删除成功", AjaxResultType.Success, null));
        }
        public ActionResult List(PagedParam<UserQuery> queryCond)
        {
           var result=  new UserService().List(queryCond);
           return Json(new AjaxResult("查询成功", AjaxResultType.Success, new { rows = result, total = result.TotalItemCount }));
        }

        public ActionResult GetById(int id)
        {
            var result = new UserService().GetById(id);
            return Json(new AjaxResult("查询成功", AjaxResultType.Success, result));
        }
        
    }
}
