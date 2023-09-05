using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delfi.Glo.Entities.Dto
{
    public class AlertsDto:DtoBaseEntity
    {
        public string WellName { get;  set; }
        public string AlertLevel { get;  set; }
        public DateTime ?TimeandDate { get;  set; }
        public string AlertDescription { get;  set; }
        public string AlertType { get;  set; }
        public string AlertStatus { get;  set; }
        public bool SnoozeFlag { get; set; }
        public string? SnoozeDateTime { get; set; }
        public int? SnoozeInterval { get; set; }
        public string? Comment { get; set; }
        public string UserId { get; set; }
    }
}
