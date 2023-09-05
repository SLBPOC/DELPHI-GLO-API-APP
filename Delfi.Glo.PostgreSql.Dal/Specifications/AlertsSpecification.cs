﻿using Delfi.Glo.Common.Services;
using Delfi.Glo.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Delfi.Glo.PostgreSql.Dal.Specifications
{
    public sealed class AlertsSpecification : Specification<AlertsDto>
    {
        private readonly string searchstring;
        public AlertsSpecification(string searchstring)
        {
            this.searchstring = searchstring;
        }
        public override Expression<Func<AlertsDto, bool>> ToExpression()
        {
            return evetns => evetns.WellName.Contains(searchstring) || evetns.AlertStatus.Contains(searchstring) || evetns.AlertType.Contains(searchstring) || evetns.AlertDescription.Contains(searchstring);

        }
    }
}