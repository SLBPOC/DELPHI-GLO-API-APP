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
        public string WellPriority { get;  set; }
        public int GLISetPoint { get; set; }
        public int QOil { get; set; }
        public int QLiq { get; set; }
        public int Qg { get; set; }
        public int Qw { get; set; }
        public decimal Wc { get;  set; }
        public decimal CompressorUpTime { get;  set; }
        public decimal ProductionUpTime { get; set; }
        public decimal DeviceUpTime { get;  set; }
        public string LastCycleStatus { get; set; }
        public DateTime TimeStamp { get;  set; }
        public int CurrentGLISetpoint { get;  set; }
        public string CurrentCycleStatus { get;  set; }
        public string ApprovalMode { get; set; }
        public string ApprovalStatus { get; set; }    
        public string UserId { get; set; }
        public int NoOfAlerts { get; set; }
        public decimal GasLiftInjectionRate { get; set; }
        public decimal DeltaPressure { get; set; }
        public decimal TubingHeadPressure { get; set; }
        public decimal FlowLinePressure { get; set; }
        public decimal CasingHeadPressure { get; set; }
    }
}
