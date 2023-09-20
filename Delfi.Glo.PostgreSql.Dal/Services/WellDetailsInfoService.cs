using Delfi.Glo.Common.Constants;
using Delfi.Glo.Common.Services;
using Delfi.Glo.Entities.Dto;
using Delfi.Glo.PostgreSql.Dal.Specifications;
using Delfi.Glo.Repository;

namespace Delfi.Glo.PostgreSql.Dal.Services
{
    public class WellDetailsInfoService : IWellDetailsInfoService<WellDetailsDto>
    {
        public async Task<WellDetailsDto> GetWellDetailsInfoById(int WellId)
        {
            WellDetailsDto wellDetailsDto = new WellDetailsDto();
            SwimLaneGraphDetails swimLaneGraphDetails = new SwimLaneGraphDetails();
            var wellsListJson = UtilityService.Read<List<WellDto>>(JsonFiles.WelldetailsInfo)?.AsQueryable();
            if (wellsListJson != null)
            {
                var wellDetailsInfoSpecification = new WellDetailSpecification(WellId);
                var well = wellsListJson.Where(wellDetailsInfoSpecification.ToExpression());
                List<WellDto> wellDto = well.ToList();

                wellDetailsDto.WellDtos = wellDto;
            }
            swimLaneGraphDetails.currentCycle = GetCurrentCycle(WellId);
            swimLaneGraphDetails.Last48HoursCycle = GetLast48HoursCycle(WellId);
            swimLaneGraphDetails.Previous48HoursCycle = GetPrevious48Hourss(WellId);

            swimLaneGraphDetails.LastCycle = GetLastCycle(WellId);
            wellDetailsDto.swimLaneGraphDetails = swimLaneGraphDetails;
            wellDetailsDto.wellSetPointDetails = Getlast7DaysSetPointDetails(WellId);
            return wellDetailsDto;
        }
        public async Task<SwimLaneGraphDetails> GetSwimLaneDetailsByDate(int WellId, DateTime StartDate, DateTime EndDate)
        {
            SwimLaneGraphDetails swimLaneGraphDetails = new SwimLaneGraphDetails();
            swimLaneGraphDetails.currentCycle = GetCurrentCycleByDate(WellId, StartDate, EndDate);
            swimLaneGraphDetails.Last48HoursCycle = GetLast48HoursCycleByDate(WellId, StartDate, EndDate);
            swimLaneGraphDetails.Previous48HoursCycle = GetPrevious48HourssByDate(WellId, StartDate, EndDate);
            swimLaneGraphDetails.LastCycle = GetLastCycleByDate(WellId, StartDate, EndDate);

            return swimLaneGraphDetails;
        }

        #region "private method"
        private static List<WellDto> GetWellSpec(int WellId)
        {
            var wellsListJson = UtilityService.Read<List<WellDto>>(JsonFiles.WelldetailsInfo)?.AsQueryable();
            var wellDetailsInfoSpecification = new WellDetailSpecification(WellId);
            var well = wellsListJson.Where(wellDetailsInfoSpecification.ToExpression());

            return well.ToList();
        }
        private static List<WellDto> GetWellSpecByDate(int WellId, DateTime StartDate, DateTime EndDate)
        {
            var wellsListJson = UtilityService.Read<List<WellDto>>(JsonFiles.WelldetailsInfo)?.AsQueryable();
            var wellDetailsInfoSpecification = new WellDetailSpecificationByDate(WellId, StartDate, EndDate);
            var well = wellsListJson.Where(wellDetailsInfoSpecification.ToExpression());

            return well.ToList();
        }
        private static List<WellSetPointDetails> Getlast7DaysSetPointDetails(int WellId)
        {
            List<WellSetPointDetails> wellSetPointDetails = new List<WellSetPointDetails>();
            var well = GetWellSpec(WellId);

            foreach (var item in well)
            {
                WellSetPointDetails wellSetPoint = new WellSetPointDetails();
                wellSetPoint.GLISetPoint = item.GLISetPoint;
                wellSetPoint.QOil = item.QOil;
                wellSetPoint.Qw = item.Qw;
                wellSetPoint.Wc = item.Wc;
                wellSetPoint.QLiq = item.QLiq;
                wellSetPoint.Qg = item.Qg;
                wellSetPointDetails.Add(wellSetPoint);
            }

            return wellSetPointDetails;
        }
        private static CurrentCycle GetCurrentCycle(int WellId)
        {
            CurrentCycle currentCycle = new CurrentCycle();

            var well = GetWellSpec(WellId);

            var recentWell = well.Where(c => c.TimeStamp.Value.Year >= DateTime.Today.Year
                                          && c.TimeStamp.Value.Month >= DateTime.Today.Month
                                          && c.TimeStamp.Value.Day >= DateTime.Today.Day);
            if (recentWell.Count() > 0)
            {

                double CUT = (double)recentWell.Select(c => c.CompressorUpTime).First();
                double PUT = (double)recentWell.Select(c => c.ProductionUpTime).First();


                currentCycle.CompressorUpTime = CUT;
                currentCycle.ProductionUpTime = PUT;
                currentCycle.TotalUpTime = (CUT >= 90) && CUT >= 90 ? "Up" : "Down";

            }
            return currentCycle;
        }
        private static CurrentCycle GetCurrentCycleByDate(int WellId, DateTime? StarDate, DateTime? EndDate)
        {
            CurrentCycle currentCycle = new CurrentCycle();

            var well = GetWellSpec(WellId);

            var recentWell = well.Where(c => c.TimeStamp.Value.Year >= EndDate.Value.Year
                                          && c.TimeStamp.Value.Month >= EndDate.Value.Month
                                          && c.TimeStamp.Value.Day >= EndDate.Value.Day);
            if (recentWell.Count() > 0)
            {

                double CUT = (double)recentWell.Select(c => c.CompressorUpTime).First();
                double PUT = (double)recentWell.Select(c => c.ProductionUpTime).First();


                currentCycle.CompressorUpTime = CUT;
                currentCycle.ProductionUpTime = PUT;
                currentCycle.TotalUpTime = (CUT >= 90) && CUT >= 90 ? "Up" : "Down";

            }
            return currentCycle;
        }
        private static Last48HoursCycle GetLast48HoursCycle(int WellId)
        {
            Last48HoursCycle last48HoursCycle = new Last48HoursCycle();
            DateTime yesterday = DateTime.Today.AddDays(-1);
            DateTime DayBeforeyesterday = DateTime.Today.AddDays(-2);

            var well = GetWellSpec(WellId);
            var Last48Hourss = well.Where(c => c.TimeStamp.Value.Year >= DayBeforeyesterday.Year
                                           && c.TimeStamp.Value.Month >= DayBeforeyesterday.Month
                                           && c.TimeStamp.Value.Day >= DayBeforeyesterday.Day

                 && c.TimeStamp.Value.Year <= yesterday.Year
                                           && c.TimeStamp.Value.Month <= yesterday.Month
                                           && c.TimeStamp.Value.Day <= yesterday.Day);
            if (Last48Hourss.Count() > 0)
            {
                var CUT = Last48Hourss.Select(c => c.CompressorUpTime).ToList();
                var PUT = Last48Hourss.Select(c => c.ProductionUpTime).ToList();
                double Compressor = (double)CUT.Average();
                double Production = (double)PUT.Average();
                last48HoursCycle.CompressorUpTime = Compressor;
                last48HoursCycle.ProductionUpTime = Production;
                last48HoursCycle.TotalUpTime = (Compressor >= 90) && Production >= 90 ? "Up" : "Down";
                last48HoursCycle.StartDate = DayBeforeyesterday;
                last48HoursCycle.EndDate = yesterday;
            }
            return last48HoursCycle;
        }
        private static Last48HoursCycle GetLast48HoursCycleByDate(int WellId, DateTime StartDate, DateTime EndDate)
        {
            Last48HoursCycle last48HoursCycle = new Last48HoursCycle();
            DateTime yesterday = EndDate.Date.AddDays(-1);
            DateTime DayBeforeyesterday = EndDate.Date.AddDays(-2);

            var well = GetWellSpecByDate(WellId, StartDate, EndDate);
            var Last48Hourss = well.Where(c => c.TimeStamp.Value.Year >= DayBeforeyesterday.Year
                                           && c.TimeStamp.Value.Month >= DayBeforeyesterday.Month
                                           && c.TimeStamp.Value.Day >= DayBeforeyesterday.Day

                 && c.TimeStamp.Value.Year <= yesterday.Year
                                           && c.TimeStamp.Value.Month <= yesterday.Month
                                           && c.TimeStamp.Value.Day <= yesterday.Day);
            if (Last48Hourss.Count() > 0)
            {
                var CUT = Last48Hourss.Select(c => c.CompressorUpTime).ToList();
                var PUT = Last48Hourss.Select(c => c.ProductionUpTime).ToList();
                double Compressor = (double)CUT.Average();
                double Production = (double)PUT.Average();
                last48HoursCycle.CompressorUpTime = Compressor;
                last48HoursCycle.ProductionUpTime = Production;
                last48HoursCycle.TotalUpTime = (Compressor >= 90) && Production >= 90 ? "Up" : "Down";

            }
            return last48HoursCycle;
        }
        private static Previous48HoursCycle GetPrevious48Hourss(int WellId)
        {
            Previous48HoursCycle previous48HoursCycle = new Previous48HoursCycle();
            DateTime Previousdaybeore = DateTime.Today.AddDays(-3);
            DateTime DayBeforeyesterdayBeforeDay = DateTime.Today.AddDays(-4);
            var well = GetWellSpec(WellId);
            var Previous48Hourss = well.Where(c => c.TimeStamp.Value.Year >= DayBeforeyesterdayBeforeDay.Year
                                           && c.TimeStamp.Value.Month >= DayBeforeyesterdayBeforeDay.Month
                                           && c.TimeStamp.Value.Day >= DayBeforeyesterdayBeforeDay.Day

                 && c.TimeStamp.Value.Year <= Previousdaybeore.Year
                                           && c.TimeStamp.Value.Month <= Previousdaybeore.Month
                                           && c.TimeStamp.Value.Day <= Previousdaybeore.Day);
            if (Previous48Hourss.Count() > 0)
            {
                var CUT = Previous48Hourss.Select(c => c.CompressorUpTime).ToList();
                var PUT = Previous48Hourss.Select(c => c.ProductionUpTime).ToList();

                double Compressor = (double)CUT.Average();
                double Production = (double)PUT.Average();
                previous48HoursCycle.CompressorUpTime = Compressor;
                previous48HoursCycle.ProductionUpTime = Production;
                previous48HoursCycle.TotalUpTime = (Compressor >= 90) && Production >= 90 ? "Up" : "Down";
                previous48HoursCycle.StartDate = DayBeforeyesterdayBeforeDay;
                previous48HoursCycle.EndDate = Previousdaybeore;
            }
            return previous48HoursCycle;
        }
        private static Previous48HoursCycle GetPrevious48HourssByDate(int WellId, DateTime StartDate, DateTime EndDate)
        {
            Previous48HoursCycle previous48HoursCycle = new Previous48HoursCycle();
            DateTime Previousdaybeore = EndDate.Date.AddDays(-3);
            DateTime DayBeforeyesterdayBeforeDay = EndDate.Date.AddDays(-4);
            var well = GetWellSpecByDate(WellId, StartDate, EndDate);
            var Previous48Hourss = well.Where(c => c.TimeStamp.Value.Year >= DayBeforeyesterdayBeforeDay.Year
                                           && c.TimeStamp.Value.Month >= DayBeforeyesterdayBeforeDay.Month
                                           && c.TimeStamp.Value.Day >= DayBeforeyesterdayBeforeDay.Day

                 && c.TimeStamp.Value.Year <= Previousdaybeore.Year
                                           && c.TimeStamp.Value.Month <= Previousdaybeore.Month
                                           && c.TimeStamp.Value.Day <= Previousdaybeore.Day);
            if (Previous48Hourss.Count() > 0)
            {
                var CUT = Previous48Hourss.Select(c => c.CompressorUpTime).ToList();
                var PUT = Previous48Hourss.Select(c => c.ProductionUpTime).ToList();

                double Compressor = (double)CUT.Average();
                double Production = (double)PUT.Average();
                previous48HoursCycle.CompressorUpTime = Compressor;
                previous48HoursCycle.ProductionUpTime = Production;
                previous48HoursCycle.TotalUpTime = (Compressor >= 90) && Production >= 90 ? "Up" : "Down";
            }
            return previous48HoursCycle;
        }
        private static LastCycle GetLastCycle(int WellId)
        {
            LastCycle lastCycle = new LastCycle();
            DateTime LastCycle6day = DateTime.Today.AddDays(-5);
            DateTime LastCyclebefore7Day = DateTime.Today.AddDays(-6);
            var well = GetWellSpec(WellId);
            var LastCycle48Hourss = well.Where(c => c.TimeStamp.Value.Year >= LastCyclebefore7Day.Year
                                           && c.TimeStamp.Value.Month >= LastCyclebefore7Day.Month
                                           && c.TimeStamp.Value.Day >= LastCyclebefore7Day.Day

                 && c.TimeStamp.Value.Year <= LastCycle6day.Year
                                           && c.TimeStamp.Value.Month <= LastCycle6day.Month
                                           && c.TimeStamp.Value.Day <= LastCycle6day.Day);
            if (LastCycle48Hourss.Count() > 0)
            {
                var CUT = LastCycle48Hourss.Select(c => c.CompressorUpTime).ToList();
                var PUT = LastCycle48Hourss.Select(c => c.ProductionUpTime).ToList();

                double Compressor = (double)CUT.Average();
                double Production = (double)PUT.Average();
                lastCycle.CompressorUpTime = Compressor;
                lastCycle.ProductionUpTime = Production;
                lastCycle.TotalUpTime = (Compressor >= 90) && Production >= 90 ? "Up" : "Down";
                lastCycle.StartDate = LastCyclebefore7Day;
                lastCycle.EndDate = LastCycle6day;
            }
            return lastCycle;
        }
        private static LastCycle GetLastCycleByDate(int WellId, DateTime StartDate, DateTime EndDate)
        {
            LastCycle lastCycle = new LastCycle();
            DateTime LastCycle6day = EndDate.Date.AddDays(-5);
            DateTime LastCyclebefore7Day = EndDate.Date.AddDays(-6);
            var well = GetWellSpecByDate(WellId, StartDate, EndDate);
            var LastCycle48Hourss = well.Where(c => c.TimeStamp.Value.Year >= LastCyclebefore7Day.Year
                                           && c.TimeStamp.Value.Month >= LastCyclebefore7Day.Month
                                           && c.TimeStamp.Value.Day >= LastCyclebefore7Day.Day

                 && c.TimeStamp.Value.Year <= LastCycle6day.Year
                                           && c.TimeStamp.Value.Month <= LastCycle6day.Month
                                           && c.TimeStamp.Value.Day <= LastCycle6day.Day);
            if (LastCycle48Hourss.Count() > 0)
            {
                var CUT = LastCycle48Hourss.Select(c => c.CompressorUpTime).ToList();
                var PUT = LastCycle48Hourss.Select(c => c.ProductionUpTime).ToList();

                double Compressor = (double)CUT.Average();
                double Production = (double)PUT.Average();
                lastCycle.CompressorUpTime = Compressor;
                lastCycle.ProductionUpTime = Production;
                lastCycle.TotalUpTime = (Compressor >= 90) && Production >= 90 ? "Up" : "Down";

            }
            return lastCycle;
        }
        private static List<WellInfoByRangeDto> GetWellInfoByRangeDtos(int WellId)
        {
            List<WellInfoByRangeDto> wellInfoByRangeDtos = new List<WellInfoByRangeDto>();
            var WelldetailsInfoJson = UtilityService.Read<List<WellInfoByRangeDto>>(JsonFiles.WellInfoByRange)?.AsQueryable();
            var wellDetailsInfoSpecification = new WellDetailsInfoSpecfication(WellId);

            var well = WelldetailsInfoJson.Where(wellDetailsInfoSpecification.ToExpression());

            wellInfoByRangeDtos = well.ToList();
            return wellInfoByRangeDtos;
        }
        #endregion
    }
}
