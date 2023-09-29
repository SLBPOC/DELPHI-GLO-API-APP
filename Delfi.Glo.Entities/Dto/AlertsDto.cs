namespace Delfi.Glo.Entities.Dto
{
    public class AlertsDto:DtoBaseEntity
    {
        public int WellId { get; set; }
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
