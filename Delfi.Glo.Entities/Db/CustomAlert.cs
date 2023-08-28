using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delfi.Glo.Entities.Db
{
    //[Table("CustomAlert")]
    public class CustomAlert:DbBaseEntity
    {
        public string WellName { get;   set; }
        public string NotificationType { get;  set; }
        public string CustomAlertName { get;  set; }
       // public string WellId { get;  set; }
        //public DateTime TimeandDate { get;  set; }
        public string Priority { get;  set; }
        public string Category { get;  set; }
        public string Operator { get;  set; }
        public string Value { get;  set; }
        public bool IsActive { get;  set; }
        public string ?CreatedBy { get;  set; }
        public string? UpdatedBy { get;  set; }
        public DateTime? CreatedDateTime { get; private set; }
        public DateTime? UpdatedDateTime { get; private set; }
        public string UserId { get; set; }
    }
}
