using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delfi.Glo.Entities.Dto
{
    public class WellDetailsDto
    {
        public List<WellDto>? WellDtos { get; set; }
        public SwimLaneGraphDetails? swimLaneGraphDetails { get; set; }

        public List< WellSetPointDetails>? wellSetPointDetails { get; set; }
         
    }
    public class SwimLaneGraphDetails
    {
        public CurrentCycle currentCycle { get; set; }
        public Last48HoursCycle Last48HoursCycle { get; set; }
        public Previous48HoursCycle Previous48HoursCycle { get; set; }
        public LastCycle LastCycle { get; set; }
    }
    public class CurrentCycle
    {
        public double CompressorUpTime { get; set; }
        public double ProductionUpTime { get; set; }
        public string TotalUpTime { get; set; }

    }
    public class Last48HoursCycle
    {
        public double CompressorUpTime { get; set; }
        public double ProductionUpTime { get; set; }
        public string TotalUpTime { get; set; }

    }
    public class Previous48HoursCycle
    {
        public double CompressorUpTime { get; set; }
        public double ProductionUpTime { get; set; }
        public string TotalUpTime { get; set; }

    }
    public class LastCycle
    {
        public double CompressorUpTime { get; set; }
        public double ProductionUpTime { get; set; }
        public string TotalUpTime { get; set; }

    }
    public class WellSetPointDetails
    {
        public int GLISetPoint { get; set; }
        public int QOil { get; set; }
        public int QLiq { get; set; }
        public int Qg { get; set; }
        public int Qw { get; set; }
        public decimal Wc { get; set; }
    }
}
