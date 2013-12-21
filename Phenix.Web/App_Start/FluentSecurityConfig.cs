using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using FluentSecurity;
using FluentSecurity.Specification.Policy.ViolationHandlers;
using NLog;
using Phenix.Core;
using Phenix.Web.Controllers;

namespace Phenix.Web
{
    public class FluentSecurityConfig
    {
        public static void ConfigureContainer()
        {
            //不支持返回string类型的action
            SecurityConfigurator.Configure(configuration =>
            {
                 configuration.GetAuthenticationStatusFrom(IsUserAuthenticated);
                 configuration.GetRolesFrom(GetCurrentUserRoles);
                 configuration.ForAllControllersInNamespaceContainingType<HomeController>().Ignore();

                configuration.ForAllControllersInNamespaceContainingType<Areas.Admin.Controllers.HomeController>()
                    .RequireAnyRole(((int) RoleEnum.Admin).ToString());
                    //必须转为int后再转为string ，要不然GetCurrentUserRoles比较时不相等             
                configuration.ForAllControllersInNamespaceContainingType<Areas.Developer.Controllers.HomeController>()
                    .DenyAnonymousAccess()
                    .RequireAnyRole(((int) RoleEnum.Admin).ToString(), ((int) RoleEnum.Developer).ToString());
                configuration.ForAllControllersInNamespaceContainingType<Areas.Advertiser.Controllers.HomeController>()
                    .DenyAnonymousAccess()
                    .RequireAnyRole(((int) RoleEnum.Admin).ToString(), ((int) RoleEnum.Advertiser).ToString());

                  //TODO 上线前取消注释 设置默认身份验证失败处理
                 //configuration.DefaultPolicyViolationHandlerIs(() => new HttpUnauthorizedPolicyViolationHandler());
            });
        }

        private static bool IsUserAuthenticated()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }

        private static IEnumerable<object> GetCurrentUserRoles()
        {
           
            var currentUser = HttpContext.Current.User;
            if (currentUser != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    var id = (FormsIdentity) HttpContext.Current.User.Identity;
                    var ticket = id.Ticket;
                    var roles = ticket.UserData.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    return roles.Cast<object>();
                }
            } 
            return null;
        }
    }
}