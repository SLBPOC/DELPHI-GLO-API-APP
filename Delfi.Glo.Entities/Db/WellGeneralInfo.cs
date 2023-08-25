using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delfi.Glo.Entities.Db
{
    [Table("GeneralInfo")]
    public class WellGeneralInfo : DbEntity
    {
        public decimal Qo { get; set; }
        public decimal Ql { get; set; }
        public decimal Qw { get; set; }
        public decimal Qg { get; set; }
        public decimal Wc { get; set; }
        public decimal GlInjectionSetPoint { get; set; }
        public decimal CompressorUpTime { get; set; }
        public decimal DeviceUpTime { get;set; }
        public string? ProcessorState { get; set; }
        public string? ApprovalMode { get; set; }
        public string? WellViewComment1 { get; set; }
        public string? WellViewComment2 { get; set; }
        public string? WellViewComment3 { get; set; }
        public string? WellViewComment4 { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set;}
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}
