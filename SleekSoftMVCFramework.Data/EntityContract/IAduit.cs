using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleekSoftMVCFramework.Data.EntityContract
{
    public interface IAduit<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        /// <summary>
        /// 
        /// </summary>
        Int64? CreatedBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
       // Int64 DeletedBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        Int64? UpdatedBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
     
        /// <summary>
        /// 
        /// </summary>
        DateTime? DateUpdated { get; set; }

        /// <summary>
        /// to manage versioning
        /// </summary>
        byte[] RowVersion { get; set; }
    

         
    }
}
