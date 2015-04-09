using System.Web;
using System.Web.Mvc;
using WebUI.Filter;

namespace WebUI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ExceptionFilter());
        }
    }
}