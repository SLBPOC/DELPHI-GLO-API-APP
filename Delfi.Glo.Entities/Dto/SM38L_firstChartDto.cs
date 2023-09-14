namespace Delfi.Glo.Entities.Dto
{
    public class SM38L_firstChartDto
    {
        public string Name { get; set; }
        public List<DataPoint> Data { get; set; }
    }
    public class DataPoint
    {
        public DateTime Date { get; set; }
        public decimal FTHP { get; set; }
    }

}
