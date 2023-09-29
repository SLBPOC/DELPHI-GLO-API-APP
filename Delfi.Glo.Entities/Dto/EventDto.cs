namespace Delfi.Glo.Entities.Dto
{
    public class EventDto: DtoBaseEntity
    {
        public int WellId { get; set; }
        public string WellName { get; set; }
        public string EventType { get; set; }
        public string EventStatus { get; set; }
        public string EventDescription { get; set; }
        public string Priority { get; set; }
        public DateTime? CreationDateTime { get; set; }
        public UserTracking? UserTracking { get; set; }
        public string UpdatedBy { get;  set; }

    }
}
