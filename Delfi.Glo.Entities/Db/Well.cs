﻿using System;
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
        public string WellName { get; set; }
        public string PumpStatus { get; set; }
        public int GLISetPoint { get; set; }
        public int QOil { get; set; }
        public int OLiq { get; set; }
        public int Og { get; set; }
        public int Ow { get; set; }
        public decimal Wc { get; set; }
        public decimal CompressorUpTime { get; set; }
        public decimal ProductionUpTime { get; set; }
        public decimal DeviceUpTime { get; set; }
        public string LastCycleStatus { get; set; }
        public DateTime TimeStamp { get; set; }
        public int CurrentGLISetpoint { get; set; }
        public string PreprocessorState { get; set; }
        public string ModeOfOperation { get; set; }
        public string CurrentCycleStatus { get; set; }
        public string UserId { get; set; }
        public int NoOfAlerts { get; set; }

        public string CreatedBy { get; private set; }
        public string UpdatedBy { get; private set; }
        public DateTime? CreatedDateTime { get; private set; }
        public DateTime? UpdatedDateTime { get; private set; }

    }
}
