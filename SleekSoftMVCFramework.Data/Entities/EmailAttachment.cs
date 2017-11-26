using SleekSoftMVCFramework.Data.EntityBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleekSoftMVCFramework.Data.Entities
{
   public class EmailAttachment : Entity<long>
    {

        public long EmailLogID { get; set; }

        [Required]
        public string FilePath { get; set; }

        [StringLength(50)]
        public string FileTitle { get; set; }

        public virtual EmailLog EmailLog { get; set; }
    }
}
