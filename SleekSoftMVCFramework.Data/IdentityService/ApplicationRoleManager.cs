using System;
using SleekSoftMVCFramework.Data.IdentityModel;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin;
using Microsoft.AspNet.Identity;
namespace SleekSoftMVCFramework.Data.IdentityService
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole, Int64>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, long> store) : base(store)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            var appRoleManager = new ApplicationRoleManager(new ApplicationRoleStore(context.Get<APPContext>()));
            return appRoleManager;
        }
    }
}
