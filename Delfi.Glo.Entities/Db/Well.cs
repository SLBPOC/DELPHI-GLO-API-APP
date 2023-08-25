using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delfi.Glo.Entities.Db
{
    [Table("Well")]
    public class Well:DbBaseEntity
    {
        public string WellName { get; private set; }
     //   public string PumpingStatus { get; private set; }
        public string PumpStatus { get; private set; }
        //public decimal Qi { get; private set; }
        //public decimal Qw { get; private set; }
        public decimal Wc { get; private set; }
        public decimal GlInjectionSetPoint { get; private set; }
        public decimal CompressorUpTime { get; private set; }
        public decimal DeviceUpTime { get; private set; }
        public DateTime TimeStamp { get; private set; }
        public int GLISetPoint { get; private set; }
        public int OLiq { get; private set; }
        public int QOil { get; private set; }
      //  public int Wc { get; private set; }
        public string LastCycleStatus { get; private set; }
        public int CurrentGLISetpoint { get; private set; }
        public string CycleStatus { get; private set; }
        public string ApprovalMode { get; private set; }
        public string ApprovalStatus { get; private set; }
    
        public string CreatedBy { get; private set; }
        public string UpdatedBy { get; private set; }
        public DateTime? CreatedDateTime { get; private set; }
        public DateTime? UpdatedDateTime { get; private set; }
       public string UserId { get; set; }
    }
}
