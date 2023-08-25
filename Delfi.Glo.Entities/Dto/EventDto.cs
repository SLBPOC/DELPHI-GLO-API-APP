using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delfi.Glo.Entities.Dto
{
    public class EventDto:DtoBaseEntity
    {
        public string WellName { get; set; }
        public string EventType { get; set; }
        public string EventStatus { get; set; }
        public string EventDescription { get; set; }
        public string Priority { get; set; }
        public DateTime? CreationDateTime { get; set; }
        public UserTracking? UserTracking { get; set; }


    }
}
