using Core.WebHelper;
using DomainService;
using Framework.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Common;

namespace WebUI.Filter
{
    public class AuthorizationFilter : FilterAttribute, IAuthorizationFilter
    {
        /// <summary>
        /// 是否验证
        /// </summary>
        public bool IsAuthorization { get; set; }
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var controller = filterContext.RouteData.Values["controller"].ToString();
            var action = filterContext.RouteData.Values["action"].ToString();
            if (controller.Equals("Home")) return;
            if (new UserService().CurrentUser == null || new UserService().CurrentCompany == null)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {

                    filterContext.Result = new JsonResult { Data = new AjaxResult("登录超时，请重新登录！", AjaxResultType.NoLogin) };
                }
                else
                {
                    filterContext.Result = new ContentResult() { Content = "登录超时，请<a href='javascript:void(0)' onclick='top.loginOpen(this)'>重新登录</a>！" };
                }
            }
        }
    }
}