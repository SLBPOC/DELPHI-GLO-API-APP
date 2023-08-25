using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delfi.Glo.Common.Helpers
{
    public class SearchCreteria
    {
        public int PageNumber { get; set; }
        public int pageSize { get; set; }
        public int skip { get; set; }

        public string searchString { get; set; }
        public string dir { get; set; }
        public string field { get; set; }
        public string Status { get; set; }
    }
}
