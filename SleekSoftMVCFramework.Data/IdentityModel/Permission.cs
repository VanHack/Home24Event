using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SleekSoftMVCFramework.Data.EntityBase;

namespace SleekSoftMVCFramework.Data.IdentityModel
{
    public class Permission : BaseEntityWithAudit<long>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        
    }
}
