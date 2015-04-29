using Core.WebHelper;
using DomainService;
using Framework.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Common;
using WebUI.Filter;

namespace WebUI.Controllers
{
    public class HomeController : BaseController
    {
        
        public ActionResult Index()
        {
            //SessionHelper.SetSession(SessionEnum.LoginUser.ToString(), new UserService().GetById(1));
            //SessionHelper.SetSession(SessionEnum.OperateCompany.ToString(), new CompanyService().GetById(1));
            return View();
        }
        public ActionResult Default()
        {
            return View();
        }
        public ActionResult Login(string LoginName, string PassWord, int companyId)
        {
            var user= new UserService().Login(LoginName, PassWord, companyId);
            return Json(new AjaxResult("登录成功", AjaxResultType.Success, user.LoginName));
        }
        public ActionResult LoginOut()
        {
            SessionHelper.ClearSession();
            return Json(new AjaxResult("退出成功", AjaxResultType.Success));
        }
        public ActionResult IsLogin()
        {
            if (new UserService().CurrentUser == null || new UserService().CurrentCompany == null)
            {
                return Json(new AjaxResult("登录成功", AjaxResultType.Success, -1));
            }
            return Json(new AjaxResult("登录成功", AjaxResultType.Success, 1));
        }
        public ActionResult GetComboboxList()
        {
            return Json(new CompanyService().GetComboboxList());
        }
    }
}
