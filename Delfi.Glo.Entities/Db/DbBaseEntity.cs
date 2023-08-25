using System.ComponentModel.DataAnnotations.Schema;

namespace Delfi.Glo.Entities.Db
{
    public class DbBaseEntity
    {
        public int Id { get; set; }
        [Column("entitytype")]
        public string EntityType { get; set; }
        [Column("timestamp")]
        public DateTime Timestamp { get; set; }

        // add other base properties if any
    }
}