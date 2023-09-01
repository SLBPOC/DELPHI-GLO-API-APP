using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
namespace Delfi.Glo.Common.Services
{
    public class DynamicSort
    {
        public static IQueryable<T> ApplyDynamicSort<T>(IQueryable<T> query, List<SortExpression> sortExpressions)
        {
            List<string> expressions = new List<string>();

            if (sortExpressions != null && sortExpressions.Count > 0)
            {
                foreach (var expr in sortExpressions)
                {
                    if (expr.dir == "asc")
                    {
                        expressions.Add(expr.field);
                    }
                    else if (expr.dir == "desc")
                    {
                        expressions.Add(expr.field + " desc");
                    }
                }
            }

            if (expressions.Count == 0)
            {
                expressions.Add("Id asc");
            }

            string expression = string.Join(",", expressions);

            return query.OrderBy(expression);
        }
    }
}
