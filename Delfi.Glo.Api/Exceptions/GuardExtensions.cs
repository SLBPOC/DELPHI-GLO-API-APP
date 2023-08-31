using Ardalis.GuardClauses;
using Delfi.Glo.Api.Exceptions.Alerts;
using Delfi.Glo.Api.Exceptions.Paging;
using Delfi.Glo.Entities.Dto;

namespace Delfi.Glo.Api.Exceptions
{
    public static class Guards
    {
        public static void InvalidPageIndex(this IGuardClause guardClause, int pageIndex)
        {
            if (pageIndex <= 0)
            {
                throw new InvalidPageIndexException(pageIndex);
            }
        }

        public static void InvalidPageSize(this IGuardClause guardClause, int pageSize)
        {
            if (pageSize <= 0)
            {
                throw new InvalidPageSizeException(pageSize);
            }
        }

        public static void NullAlerts(this IGuardClause guardClause, AlertsDto alerts)
        {
            if (alerts == null)
                throw new AlertNotFoundException();
        }
    }
}