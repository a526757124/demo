using System.Web.Mvc;

namespace CTS.Areas.ReceiptManagement
{
    public class ReceiptManagementAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ReceiptManagement";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ReceiptManagement_default",
                "ReceiptManagement/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
