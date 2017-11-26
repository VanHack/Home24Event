using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SleekSoftMVCFramework.Data.EntityBase;

namespace SleekSoftMVCFramework.Data.IdentityModel
{
    public  class ApplicationUserPasswordHistory: BaseEntityWithAudit<long>
    {
        public long UserId { get; set; }
        public string HashPassword { get; set; }

        public string PasswordSalt { get; set; }
    }
}
