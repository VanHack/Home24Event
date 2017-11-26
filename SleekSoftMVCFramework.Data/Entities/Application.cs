using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SleekSoftMVCFramework.Data.EntityBase;

namespace SleekSoftMVCFramework.Data.Entities
{
   public  class Application :Entity<int>
    {
        public string ApplicationName { get; set; }
        public string Description { get; set; }

        public string TermsAndConditions { get; set; }

        public Boolean HasAdminUserConfigured { get; set; }
    }
}
