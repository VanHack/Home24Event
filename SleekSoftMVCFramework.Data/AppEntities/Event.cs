using SleekSoftMVCFramework.Data.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleekSoftMVCFramework.Data.AppEntities
{
    public class Event : BaseEntityWithAudit<Int64>
    {
        public string EventName { get; set; }

        public string EventDescription { get; set; }

        public string Venue { get; set; }

        public Int64 ArtistId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string City { get; set; }

        public string Country { get; set; }


    }
}
