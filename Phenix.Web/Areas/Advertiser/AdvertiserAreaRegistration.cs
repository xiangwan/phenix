using System.Web.Mvc;

namespace Phenix.Web.Areas.Advertiser
{
    public class AdvertiserAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Advertiser"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Advertiser_default",
                "Advertiser/{controller}/{action}/{id}",
                new {action = "Index", id = UrlParameter.Optional}
                );
        }
    }
}