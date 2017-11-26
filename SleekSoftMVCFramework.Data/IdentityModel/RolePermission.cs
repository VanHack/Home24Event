using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SleekSoftMVCFramework.Data.EntityBase;

namespace SleekSoftMVCFramework.Data.IdentityModel
{
    public class RolePermission : BaseEntityWithAudit<long>
    {
        public long PermissionId { get; set; }
        public long RoleId { get; set; }
    }
}
