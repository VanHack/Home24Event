using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleekSoftMVCFramework.Data.Constant
{
    public class AppConstant
    {
        public const string PortalAdminRole = "PortalAdmin";
        public const string ChurchAdminAccountRole = "ChurchAdmin";
        public const string ChurchPastorRole = "ChurchPastor";
        public const string ResellerAccountRole = "ResellerAccount";
        public const string SubAccountRole = "SubAccount";



        public const string FetchUserPermissionAndRole = "spFetchUserPermissionAndRole";
        public const string DeleteRecentPassword = "spPasswordHistoryDeleteNonRecentPasswords";
        public const string FetchUserRoleByUserId = "SpGetUserRole";
        public const string DeleteUserRoleByUserId = "SpDeleteUserRoleByUserId";

        public const string GetAccountAlertSettingByUserId = "SpGetAccountAlertSettingByUserId";
    }
}
