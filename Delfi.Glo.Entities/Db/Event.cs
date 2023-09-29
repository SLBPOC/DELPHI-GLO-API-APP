using System.ComponentModel.DataAnnotations.Schema;

namespace Delfi.Glo.Entities.Db
{
    [Table("Event")]
    public class Event:DbBaseEntity
    {
        public string WellName { get; private set; }
        public string EventType { get; private set; }
        public string EventStatus { get; private set; }
        public string EventDescription { get; private set; }
        public string Priority { get; private set; }
        public DateTime? CreationDateTime { get; set; }
        public string CreatedBy { get; private set; }
        public string UpdatedBy { get; private set; }
        public DateTime? CreatedDateTime { get; private set; }
        public DateTime? UpdatedDateTime { get; private set; }
        public string UserId { get; set; }


    }
}
