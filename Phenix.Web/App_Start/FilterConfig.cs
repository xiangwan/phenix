using System.Web;
using System.Web.Mvc;
using FluentSecurity;

namespace Phenix.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new HandleSecurityAttribute(), -1);
        }
    }
}