using System.Web.Mvc;

namespace CTS.Areas.SendManagement
{
    public class SendManagementAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "SendManagement";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "SendManagement_default",
                "SendManagement/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
