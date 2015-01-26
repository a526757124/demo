using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;

namespace CTS.App_Code
{
    public class BaseController : Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {
            if (IsAjaxRequest(filterContext.HttpContext))
            {
                filterContext.ExceptionHandled = true;

                // Fix for IE: http://malsup.com/jquery/form/#file-upload
                if (filterContext.HttpContext.Request["FromJqueryForm"] != null)
                {
                    Response.StatusCode = (int)HttpStatusCode.OK;
                    string response = JsonConvert.SerializeObject(new { error = filterContext.Exception.Message });
                    filterContext.Result = Content(response, MediaTypeNames.Text.Plain);
                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    filterContext.Result = Content(filterContext.Exception.Message, MediaTypeNames.Text.Plain);
                }
            }
            //LogManager.GetLogger(filterContext.Controller.GetType()).Error(string.Format("{0} {1}", Request.HttpMethod, Request.RawUrl), filterContext.Exception);
            base.OnException(filterContext);
        }
        private static bool IsAjaxRequest(HttpContextBase context) { return context.Request.IsAjaxRequest() || context.Request["AjaxRequest"] != null; }
    }
}