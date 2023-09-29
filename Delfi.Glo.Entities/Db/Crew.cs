using System.ComponentModel.DataAnnotations.Schema;

namespace Delfi.Glo.Entities.Db
{
    [Table("Crew")]
    public class Crew : DbBaseEntity
    {
        public string? CrewName { get; set; }
    }
}
