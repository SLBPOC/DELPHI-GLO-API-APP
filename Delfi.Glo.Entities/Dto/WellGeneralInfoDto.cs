using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delfi.Glo.Entities.Dto
{
    public class WellGeneralInfoDto:DtoBaseEntity
    {
        public decimal Qo { get; set; }
        public decimal Ql { get; set; }
        public decimal Qw { get; set; }
        public decimal Qg { get; set; }
        public decimal Wc { get; set; }
        public decimal GlInjectionSetPoint { get; set; }
        public decimal CompressorUpTime { get; set; }
        public decimal DeviceUpTime { get; set; }
        public string ProcessorState { get; set; }
        public string ApprovalMode { get; set; }
        public string WellViewComment1 { get; set; }
        public string WellViewComment2 { get; set; }
        public string WellViewComment3 { get; set; }
        public string WellViewComment4 { get; set; }
        
    }
}
