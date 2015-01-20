using System.Web.Mvc;

namespace CTS.Areas.SystemSettings
{
    public class SystemSettingsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "SystemSettings";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "SystemSettings_default",
                "SystemSettings/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
