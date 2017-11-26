using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SleekSoftMVCFramework.ViewModel
{
    public class EventViewModel
    {
        public long Id { get; set; }
        public string EventName { get; set; }

        public string EventDescription { get; set; }

        public string Venue { get; set; }

        public Int64 ArtistId { get; set; }


        public IEnumerable<System.Web.Mvc.SelectListItem> Artists { get; set; }
        
        
        public string ArtistName { get; set; }
        public string StartDate { get; set; }

        public string EndDate { get; set; }

        

        public string City { get; set; }

        public string Country { get; set; }

    }
}