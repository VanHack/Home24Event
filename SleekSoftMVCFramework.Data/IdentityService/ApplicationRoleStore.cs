using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using SleekSoftMVCFramework.Data.IdentityModel;

namespace SleekSoftMVCFramework.Data.IdentityService
{
    public class ApplicationRoleStore : RoleStore<ApplicationRole, Int64, ApplicationUserRole>
    {
        public ApplicationRoleStore(APPContext context) : base(context)
        {
        }
    }
}
