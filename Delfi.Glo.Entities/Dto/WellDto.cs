using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delfi.Glo.Entities.Dto
{
    public class WellDto
    {
        [Column("id", TypeName = "int")]
        public int Id { get; set; }
        public string WellName { get;  set; }
        public string WellPriority { get;  set; }
        public int GLISetPoint { get; set; }
        public int QOil { get; set; }
        public int QLiq { get; set; }
        public int Qg { get; set; }
        public int Qw { get; set; }
        public decimal Wc { get;  set; }
        public double? CompressorUpTime { get; set; }
        public double? ProductionUpTime { get; set; }
        public decimal DeviceUpTime { get;  set; }
        public string LastCycleStatus { get; set; }
        public DateTime? TimeStamp { get;  set; }
        public int CurrentGLISetpoint { get;  set; }
        public string CurrentCycleStatus { get;  set; }
        public string ApprovalMode { get; set; }
        public string ApprovalStatus { get; set; }    
        public string UserId { get; set; }
        public int NoOfAlerts { get; set; }
        public int GOR { get; set; }
        public int GLIR { get; set; }
        public int DP { get; set; }
        public int THP { get; set; }
        public int FLP { get; set; }
        public int CHP { get; set; }
    }
    public class WellChartDetails
    {
        public decimal value { get; set; }
        public object[][]? data { get; set; }
    }
}
