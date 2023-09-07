﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delfi.Glo.Repository
{
    public interface IGeneralInfoService<T> where T : class
    {
        Task<T> GetAsync(int id);
        //Task<IEnumerable<T>> GetAllAsync();
       
    }
}