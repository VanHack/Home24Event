using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SleekSoftMVCFramework.Data.EntityContract;
using SleekSoftMVCFramework.Data.IdentityModel;

namespace SleekSoftMVCFramework.Data.EntityBase
{
    public class BaseEntityWithAudit<TPrimaryKey> : IAduit<TPrimaryKey>
    {
        public BaseEntityWithAudit()
        {
            IsActive = true;
            DateCreated=DateTime.Now;
            IsDeleted = false;
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TPrimaryKey Id { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public Int64? CreatedBy { get; set; }
        public Int64? UpdatedBy { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; } 

        [Timestamp]
        public byte[] RowVersion { get; set; }
        public bool IsTransient()
        {
            return EqualityComparer<TPrimaryKey>.Default.Equals(Id, default(TPrimaryKey));
            
        }
       // public virtual ApplicationUser ApplicationUser { get; set; }
    }
}