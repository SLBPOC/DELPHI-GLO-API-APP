using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delfi.Glo.Entities.Dto
{
    public class WellInfoByRangeDto:DtoBaseEntity
    {
        public string WellName { get; set; }
        public string WellId { get; set; }
        public int GLISetPoint { get; set; }
        public decimal Ql { get; set; }
        public decimal Qo { get; set; }
        public decimal Wc { get; set; }
        public string WellStatus { get; set; }
        public bool Production { get; set; }
        public bool GasLift { get; set; }
        public int ComputedSetPoint { get; set; }
        public string SetPointType { get; set; }
        public string ApproveStatus { get; set; }
        public string WellCycle { get; set; }
        public string TimeStamp { get; set; }
   
    }
}
