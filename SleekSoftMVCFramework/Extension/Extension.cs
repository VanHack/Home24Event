using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SleekSoftMVCFramework.Data.IdentityModel;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using SleekSoftMVCFramework.Data.IdentityService;

namespace SleekSoftMVCFramework.Extension
{

    public enum EnumStatus
    {
        Inactive = 0,
        Active = 1
    }

   
    public static class Extension
    {
        public static ApplicationUser applicationUserInformation(long UserId)
        {
            if (UserId == 0)
            {
                return new ApplicationUser();
            }
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext()
                .GetUserManager<ApplicationUserManager>().FindById(UserId);

            return user;
           
        }


        public static string IPAddress()
        {
            return System.Web.HttpContext.Current.Request.UserHostAddress;
        }

        public enum  Status
        {
            Inactive,
            Active
        }

        public static List<KeyValuePair<string, string>> StatusList()
        {
            List<KeyValuePair<string, string>> orderList = new List<KeyValuePair<string, string>>();
            foreach (var value in Enum.GetValues(typeof(Status)))
            {
                orderList.Add(new KeyValuePair<string, string>(((int)value).ToString(), ((Status)value).ToString()));
            }
            return orderList;
        }
    }
}