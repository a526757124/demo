using System.Web.Mvc;

namespace WebUI.Areas.VoucherManagement
{
    public class VoucherManagementAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "VoucherManagement";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "VoucherManagement_default",
                "VoucherManagement/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
