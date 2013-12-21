using System.Web.Mvc;

namespace Phenix.Web.Areas.Developer
{
    public class DeveloperAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Developer"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Developer_default",
                "Developer/{controller}/{action}/{id}",
                new {action = "Index", id = UrlParameter.Optional}
                );
        }
    }
}