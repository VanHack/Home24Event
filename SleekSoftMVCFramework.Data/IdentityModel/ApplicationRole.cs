using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SleekSoftMVCFramework.Data.EntityContract;

namespace SleekSoftMVCFramework.Data.IdentityModel
{
    public class ApplicationRole : IdentityRole<long, ApplicationUserRole>, IEntity<Int64>
    {
        public ApplicationRole()
        {
            DateCreated = DateTime.Now;
            IsActive = true;
            IsDeleted = false;
        }
        public DateTime DateCreated { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsTransient()
        {
            return EqualityComparer<Int64>.Default.Equals(Id, default(Int64));
        }
    }
}
