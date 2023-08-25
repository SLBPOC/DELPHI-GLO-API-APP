using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delfi.Glo.Entities.Dto
{
    public class WellDto:DtoBaseEntity
    {
        public string WellName { get;  set; }
        //public string PumpingStatus { get;  set; }
        public string PumpStatus { get;  set; }
        //public decimal Qi { get;  set; }
        //public decimal Qw { get;  set; }
        public decimal Wc { get;  set; }
        public decimal GlInjectionSetPoint { get;  set; }
        public decimal CompressorUpTime { get;  set; }
        public decimal DeviceUpTime { get;  set; }
        public DateTime TimeStamp { get;  set; }
        public int GLISetPoint { get;  set; }
        public int OLiq { get;  set; }
        public int QOil { get;  set; }
        //  public int Wc { get; private set; }
        public string LastCycleStatus { get;  set; }
        public int CurrentGLISetpoint { get;  set; }
        public string CycleStatus { get;  set; }
        public string ApprovalMode { get;  set; }
        public string ApprovalStatus { get;  set; }
        public string UserId { get; set; }

    }
}
