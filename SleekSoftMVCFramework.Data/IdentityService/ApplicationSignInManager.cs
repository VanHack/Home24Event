using System;
using SleekSoftMVCFramework.Data.IdentityModel;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace SleekSoftMVCFramework.Data.IdentityService
{
    public class ApplicationSignInManager : SignInManager<ApplicationUser, Int64>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
           : base(userManager, authenticationManager)
        {
        }
    }
}
