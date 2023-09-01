﻿using Delfi.Glo.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delfi.Glo.Common;
using Delfi.Glo.Common.Services;

namespace Delfi.Glo.Repository
{
    public interface IUniversityService<T> where T : class
    {
        Task<IEnumerable<T>> GetUniversities(int pageIndex, int pageSize, string? universityByName,List<SortExpression> sortExpression);
    }
}
