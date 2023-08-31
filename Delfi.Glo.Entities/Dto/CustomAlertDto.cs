﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delfi.Glo.Entities.Dto
{
    public class CustomAlertDto:DtoBaseEntity
    {
        public string WellName { get;  set; }
       public string CustomAlertName { get; set; }
        //public string WellId { get; set; }
        public string NotificationType { get;  set; }

        //public DateTime TimeandDate { get;  set; }
        public string Priority { get;  set; }
        public string Category { get;  set; }
        public string Operator { get;  set; }
        public string Value { get;  set; }
        public bool IsActive { get;  set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        
    }
}
