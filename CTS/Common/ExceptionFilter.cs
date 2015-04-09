using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CTS.Common
{
    public class ExceptionFilter : FilterAttribute, IExceptionFilter
    {

        public void OnException(ExceptionContext filterContext)
        {
            Exception exception = filterContext.Exception;
            string message;
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                message = "Ajax访问时引发异常：";
                AjaxResult ajaxResult = null;
                if (exception is BusinessException)
                {
                    message = exception.Message;
                    ajaxResult = new AjaxResult(message, AjaxResultType.Warning);
                }
                else if (exception is HttpAntiForgeryException)
                {
                    message += "安全性验证失败。<br>请刷新页面重试，详情请查看系统日志。";
                }
                else
                {
                    message += exception.Message;
                }

                filterContext.Result = new JsonResult() { Data = ajaxResult ?? new AjaxResult(message, AjaxResultType.Error) };
                filterContext.ExceptionHandled = true;
            }
            else
            {
                filterContext.Result = new ContentResult() { Content = "系统异常:" + exception.Message };
            }
        }
    }
}