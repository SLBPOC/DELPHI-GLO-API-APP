using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delfi.Glo.Entities.Db
{
    [Table("Alerts")]
    public class Alerts:DbBaseEntity
    {
        public string WellName { get; private set; }
        public string AlertLevel { get; private set; }
        public DateTime TimeandDate { get; private set; }
        public string AlertDescription { get; private set; }
        public string AlertType { get; private set; }
        public string AlertStatus { get; private set; }
        public string CreatedBy { get; private set; }
        public string UpdatedBy { get; private set; }
        public DateTime? CreatedDateTime { get; private set; }
        public DateTime? UpdatedDateTime { get; private set; }
        public string UserId { get; set; }
    }
}
